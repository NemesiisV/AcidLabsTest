using System;
using System.Collections.Generic;

using Amazon.DynamoDBv2.Model;

using AcidLabsService.Frameworks.DataReadOnly;
using AcidLabsService.Models.Responses;
using System.Linq;

namespace AcidLabsService.Frameworks.Mappers
{
    public static class UsersMapper
    {
        public static GetUserResponse ToResponse(this Dictionary<string, AttributeValue> keyValuePairs)
        {
            return new GetUserResponse()
            {
                Id = new Guid(keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.Id).S),
                Rut = keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.Rut).S,
                FirstName = keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.FirstName).S,
                LastName = keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.LastName).S,
                Email = keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.Email).S
            };
        }

        public static IEnumerable<GetUserResponse> ToListResponse(this IEnumerable<Dictionary<string, AttributeValue>> keyValuePairsList)
        {
            List<GetUserResponse> userResponses = new List<GetUserResponse>();

            if (keyValuePairsList != null && keyValuePairsList.Any())
            {
                foreach (var keyValuePair in keyValuePairsList)
                {
                    userResponses.Add(keyValuePair.ToResponse());
                }
            }

            return userResponses;
        }
    }
}
