using Amazon.DynamoDBv2;

namespace AcidLabsTest.Service.Frameworks.ExternalServices.ServiceContracts
{
    public interface IAmazonDynamoDbClientExtension
    {
        public AmazonDynamoDBClient GetAmazonDynamoDB();
    }
}
