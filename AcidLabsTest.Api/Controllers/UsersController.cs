using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using AcidLabsTest.Service.Models.Requests;
using AcidLabsTest.Service.Models.Responses;
using AcidLabsTest.Service.ServiceContracts;

namespace AcidLabsTest.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public sealed class UsersController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [SwaggerOperation("AddUserAsync")]
        [SwaggerResponse((int) HttpStatusCode.OK, "Ok", typeof(void))]
        public async Task<Guid> AddUserAsync([FromBody] AddUserRequest addUserRequest)
        {
            return await _usersService.AddUserAsync(addUserRequest).ConfigureAwait(false);
        }

        [HttpGet, Route("email/{userEmail}")]
        [SwaggerOperation("GetUserIdByEmailAsync")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ok", typeof(void))]
        public async Task<Guid> GetUserIdByEmailAsync([FromRoute] string userEmail)
        {
            return await _usersService.GetUserIdByEmailAsync(userEmail).ConfigureAwait(false);
        }

        [HttpGet, Route("{userId}")]
        [SwaggerOperation("GetUserAsync")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ok", typeof(GetUserResponse))]
        public async Task<GetUserResponse> GetUserAsync([FromRoute] Guid userId)
        {
            return await _usersService.GetUserAsync(userId).ConfigureAwait(false);
        }

        [HttpGet]
        [SwaggerOperation("GetAllUsersAsync")]
        [SwaggerResponse((int) HttpStatusCode.OK, "Ok", typeof(IEnumerable<GetUserResponse>))]
        public async Task<IEnumerable<GetUserResponse>> GetAllUserAsync()
        {
            return await _usersService.GetAllUsersAsync().ConfigureAwait(false);
        }

        [HttpPut, Route("{userId}")]
        [SwaggerOperation("UpdateUserAsync")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ok", typeof(void))]
        public async Task UpdateUserAsync([FromRoute] Guid userId, [FromBody] UpdateUserRequest updateUserRequest)
        {
            await _usersService.UpdateUserAsync(userId, updateUserRequest).ConfigureAwait(false);
        }

        [HttpDelete, Route("{userId}")]
        [SwaggerOperation("DeleteUserAsync")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ok", typeof(void))]
        public async Task DeleteUserAsync([FromRoute] Guid userId)
        {
            await _usersService.DeleteUserAsync(userId).ConfigureAwait(false);
        }
    }
}
