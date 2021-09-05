using System;

using AcidLabsTest.Service.Models.Bases;

namespace AcidLabsTest.Service.Models.Responses
{
    public sealed class GetUserResponse : UserBase
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
