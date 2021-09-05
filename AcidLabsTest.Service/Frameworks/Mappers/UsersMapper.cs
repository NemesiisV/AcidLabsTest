using System;
using System.Collections.Generic;

using Amazon.DynamoDBv2.Model;

using AcidLabsTest.Service.Frameworks.DataReadOnly;
using AcidLabsTest.Service.Models.Responses;

namespace AcidLabsTest.Service.Frameworks.Mappers
{
    public static class UsersMapper
    {
        public static GetUserResponse ToResponse(this Dictionary<string, AttributeValue> keyValuePairs)
        {
            return new()
            {
                Id = new Guid(keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.Id).S),
                Rut = keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.Rut).S,
                FirstName = keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.FirstName).S,
                LastName = keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.LastName).S,
                Email = keyValuePairs.GetValueOrDefault(AmazonDynamoDbUserData.Email).S
            };
        }
    }
}
