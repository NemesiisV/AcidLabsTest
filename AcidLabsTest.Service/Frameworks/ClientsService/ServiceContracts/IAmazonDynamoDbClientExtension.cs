using Amazon.DynamoDBv2;

namespace AcidLabsTest.Service.Frameworks.ClientsService.ServiceContracts
{
    public interface IAmazonDynamoDbClientExtension
    {
        public AmazonDynamoDBClient GetAmazonDynamoDB();
    }
}
