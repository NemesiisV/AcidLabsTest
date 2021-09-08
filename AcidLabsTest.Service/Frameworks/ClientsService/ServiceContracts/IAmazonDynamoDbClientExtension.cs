using Amazon.DynamoDBv2;

namespace AcidLabsService.Frameworks.ClientsService.ServiceContracts
{
    public interface IAmazonDynamoDbClientExtension
    {
        public AmazonDynamoDBClient GetAmazonDynamoDB();
    }
}
