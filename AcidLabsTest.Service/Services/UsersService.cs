using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

using AcidLabsService.Frameworks.ExternalServices.ServiceContracts;
using AcidLabsService.Frameworks.DataReadOnly;
using AcidLabsService.Frameworks.Mappers;
using AcidLabsService.Models.Requests;
using AcidLabsService.Models.Responses;
using AcidLabsService.ServiceContracts;

namespace AcidLabsService.Services
{
    public sealed class UsersService : IUsersService
    {
        private readonly AmazonDynamoDBClient _amazonDynamoDbClient;

        public UsersService(IAmazonDynamoDbClientExtension amazonDynamoDbClientExtension)
        {
            _ = amazonDynamoDbClientExtension ?? throw new ArgumentNullException(nameof(amazonDynamoDbClientExtension));
            _amazonDynamoDbClient = amazonDynamoDbClientExtension.GetAmazonDynamoDB();
        }

        public async Task<Guid> AddUserAsync(AddUserRequest addUserRequest)
        {
            _ = addUserRequest ?? throw new ArgumentNullException(nameof(addUserRequest));
            var usersByEmail = await GetUserByEmailAsync(addUserRequest.Email).ConfigureAwait(false);

            if (usersByEmail.Any())
            {
                throw new ResourceInUseException(nameof(addUserRequest.Email));
            }

            var userId = Guid.NewGuid();

            PutItemRequest putItemRequest = new PutItemRequest()
            {
                TableName = AmazonDynamoDbUserData.UserTable,
                Item = new Dictionary<string, AttributeValue>()
                {
                    { AmazonDynamoDbUserData.Id, new AttributeValue { S = userId.ToString() }},
                    { AmazonDynamoDbUserData.Rut, new AttributeValue { S = addUserRequest.Rut }},
                    { AmazonDynamoDbUserData.FirstName, new AttributeValue { S = addUserRequest.FirstName }},
                    { AmazonDynamoDbUserData.LastName, new AttributeValue { S = addUserRequest.LastName }},
                    { AmazonDynamoDbUserData.Email, new AttributeValue { S = addUserRequest.Email }}
                }
            };

            var result = await _amazonDynamoDbClient.PutItemAsync(putItemRequest).ConfigureAwait(false);

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new InternalServerErrorException(result.ResponseMetadata.ToString());
            }

            return userId;
        }

        public async Task<Guid> GetUserIdByEmailAsync(string userEmail)
        {
            _ = userEmail ?? throw new ArgumentNullException(nameof(userEmail));

            var usersByEmail = await GetUserByEmailAsync(userEmail).ConfigureAwait(false);

            if (!usersByEmail.Any() || usersByEmail.Count > 1)
            {
                throw new KeyNotFoundException(AmazonDynamoDbUserData.EmailNotExists(userEmail));
            }

            var user = usersByEmail.SingleOrDefault();
            user.TryGetValue(AmazonDynamoDbUserData.Id, out var resultId);
            Guid userId = Guid.Parse(resultId.S);

            return userId;
        }

        public async Task<GetUserResponse> GetUserAsync(Guid userId)
        {
            var userItem = await GetUserByIdAsync(userId).ConfigureAwait(false);

            return userItem.ToResponse();
        }

        public async Task<IEnumerable<GetUserResponse>> GetAllUsersAsync()
        {
            ScanRequest scanRequest = new ScanRequest
            {
                TableName = AmazonDynamoDbUserData.UserTable
            };

            ScanResponse scanResponse = await _amazonDynamoDbClient.ScanAsync(scanRequest).ConfigureAwait(false);

            if (!scanResponse.Items.Any())
            {
                throw new InternalServerErrorException(AmazonDynamoDbUserData.EmptyTable);
            }

            return scanResponse.Items.ToListResponse();
        }

        public async Task UpdateUserAsync(Guid userId, UpdateUserRequest updateUserRequest)
        {
            _ = updateUserRequest ?? throw new ArgumentNullException(nameof(updateUserRequest));
            _ = await GetUserByIdAsync(userId).ConfigureAwait(false);

            UpdateItemRequest updateItemRequest = new UpdateItemRequest()
            {
                TableName = AmazonDynamoDbUserData.UserTable,
                Key = new Dictionary<string, AttributeValue>()
                {
                    { AmazonDynamoDbUserData.Id, new AttributeValue { S = userId.ToString() }},
                },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    { AmazonDynamoDbUserData.RutExpressionName, AmazonDynamoDbUserData.Rut },
                    { AmazonDynamoDbUserData.FirstNameExpressionName, AmazonDynamoDbUserData.FirstName },
                    { AmazonDynamoDbUserData.LastNameExpressionName, AmazonDynamoDbUserData.LastName }
                },
                ExpressionAttributeValues= new Dictionary<string, AttributeValue>()
                {
                    { AmazonDynamoDbUserData.RutExpressionValue, new AttributeValue { S = updateUserRequest.Rut} },
                    { AmazonDynamoDbUserData.FirstNameExpressionValue, new AttributeValue { S = updateUserRequest.FirstName} },
                    { AmazonDynamoDbUserData.LastNameExpressionValue, new AttributeValue { S = updateUserRequest.LastName} },
                },
                UpdateExpression = AmazonDynamoDbUserData.UpdateExpression
            };

            var result = await _amazonDynamoDbClient.UpdateItemAsync(updateItemRequest).ConfigureAwait(false);

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new InternalServerErrorException(result.ResponseMetadata.ToString());
            }
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var userItem = await GetUserByIdAsync(userId).ConfigureAwait(false);
            var user = userItem.ToResponse();

            if (user.Email.Equals(AmazonDynamoDbUserData.FranciscaEmailReserved, StringComparison.OrdinalIgnoreCase))
            {
                throw new FieldAccessException(AmazonDynamoDbUserData.ReservedUserCannotBeDelete(user.Email));
            }

            DeleteItemRequest deleteItemRequest = new DeleteItemRequest
            {
                TableName = AmazonDynamoDbUserData.UserTable,
                Key = new Dictionary<string, AttributeValue>()
                {
                    { AmazonDynamoDbUserData.Id, new AttributeValue { S = userId.ToString() } }
                }
            };

            var result = await _amazonDynamoDbClient.DeleteItemAsync(deleteItemRequest).ConfigureAwait(false);

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new InternalServerErrorException(result.ResponseMetadata.ToString());
            }
        }

        private async Task<List<Dictionary<string, AttributeValue>>> GetUserByEmailAsync(string userEmail)
        {
            QueryRequest queryRequest = new QueryRequest() {
                TableName = AmazonDynamoDbUserData.UserTable,
                IndexName = AmazonDynamoDbUserData.Email,
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    { AmazonDynamoDbUserData.EmailExpressionName, AmazonDynamoDbUserData.Email }
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    { AmazonDynamoDbUserData.EmailExpressionValue, new AttributeValue { S = userEmail } }
                },
                KeyConditionExpression = AmazonDynamoDbUserData.GetByEmailExpression
            };

            QueryResponse response = await _amazonDynamoDbClient.QueryAsync(queryRequest).ConfigureAwait(false);

            return response.Items;
        }

        private async Task<Dictionary<string, AttributeValue>> GetUserByIdAsync(Guid userId)
        {
            GetItemRequest getItemRequest = new GetItemRequest()
            {
                TableName = AmazonDynamoDbUserData.UserTable,
                Key = new Dictionary<string, AttributeValue>()
    {
                    {
                        AmazonDynamoDbUserData.Id, new AttributeValue { S = userId.ToString() }
                    }
                },
            };

            GetItemResponse response = await _amazonDynamoDbClient.GetItemAsync(getItemRequest).ConfigureAwait(false);

            if (!response.Item.Any())
            {
                throw new KeyNotFoundException(AmazonDynamoDbUserData.IdNotExists(userId.ToString()));
            }

            return response.Item;
        }
    }
}
