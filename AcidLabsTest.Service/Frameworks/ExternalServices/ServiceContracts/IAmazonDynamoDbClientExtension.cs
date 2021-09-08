using Amazon.DynamoDBv2;

namespace AcidLabsService.Frameworks.ExternalServices.ServiceContracts
{
    public interface IAmazonDynamoDbClientExtension
    {
        public AmazonDynamoDBClient GetAmazonDynamoDB();
    }
}
