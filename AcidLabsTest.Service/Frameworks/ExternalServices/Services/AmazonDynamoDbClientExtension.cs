using Amazon.DynamoDBv2;
using Microsoft.Extensions.Configuration;

using AcidLabsService.Frameworks.ExternalServices.ServiceContracts;

namespace AcidLabsService.Frameworks.ExternalServices.Services
{
    public sealed class AmazonDynamoDbClientExtension : IAmazonDynamoDbClientExtension
    {
        public readonly IConfiguration _configuration;

        public AmazonDynamoDbClientExtension(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AmazonDynamoDBClient GetAmazonDynamoDB()
        {
            return new AmazonDynamoDBClient(_configuration["Aws:DynamoDbCredentials:AwsAccessKeyId"], _configuration["Aws:DynamoDbCredentials:AwsSecretKey"]);
        }
    }
}
