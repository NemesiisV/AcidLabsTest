using System;
using System.Threading.Tasks;

using AcidLabsTest.Service.Models.Requests;
using AcidLabsTest.Service.Models.Responses;
using AcidLabsTest.Service.ServiceContracts;

namespace AcidLabsTest.Service.Services
{
    public sealed class UsersService : IUsersService
    {
        public async Task AddUserAsync(AddUserRequest addUserRequest)
        {
        }

        public async Task<Guid> GetUserIdByEmailAsync(string userEmail)
        {
            return Guid.NewGuid();
        }

        public async Task<GetUserResponse> GetUserAsync(Guid userId)
        {
            return new GetUserResponse();
        }

        public async Task UpdateUserAsync(Guid userId)
        {
        }

        public async Task DeleteUserAsync(Guid userId)
        {
        }
    }
}
