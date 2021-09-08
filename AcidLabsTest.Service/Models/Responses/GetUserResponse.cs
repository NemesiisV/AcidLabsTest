using System;

using AcidLabsService.Models.Bases;

namespace AcidLabsService.Models.Responses
{
    public sealed class GetUserResponse : UserBase
    {
        public Guid Id { get; set; }
    }
}
