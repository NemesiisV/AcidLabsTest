using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using AcidLabsTest.Service.Models.Requests;
using AcidLabsTest.Service.Models.Responses;
using AcidLabsTest.Service.ServiceContracts;

namespace AcidLabsTest.Api.Controllers
{
    //[Authorize]
    [Route("api/users")]
    public sealed class UsersController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task AddUserAsync(AddUserRequest addUserRequest)
        {
            await _usersService.AddUserAsync(addUserRequest).ConfigureAwait(false);
        }

        public async Task<Guid> GetUserIdByEmailAsync(string userEmail)
        {
            return await _usersService.GetUserIdByEmailAsync(userEmail).ConfigureAwait(false);
        }

        public async Task<GetUserResponse> GetUserAsync(Guid userId)
        {
            return await _usersService.GetUserAsync(userId).ConfigureAwait(false);
        }

        public async Task UpdateUserAsync(Guid userId)
        {
            await _usersService.UpdateUserAsync(userId).ConfigureAwait(false);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            await _usersService.DeleteUserAsync(userId).ConfigureAwait(false);
        }
    }
}
