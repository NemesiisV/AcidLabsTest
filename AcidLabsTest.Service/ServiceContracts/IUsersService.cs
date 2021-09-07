using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AcidLabsTest.Service.Models.Requests;
using AcidLabsTest.Service.Models.Responses;

namespace AcidLabsTest.Service.ServiceContracts
{
    public interface IUsersService
    {
        Task<Guid> AddUserAsync(AddUserRequest addUserRequest);
        Task<Guid> GetUserIdByEmailAsync(string userEmail);
        Task<GetUserResponse> GetUserAsync(Guid userId);
        Task<IEnumerable<GetUserResponse>> GetAllUsersAsync();
        Task UpdateUserAsync(Guid userId, UpdateUserRequest updateUserRequest);
        Task DeleteUserAsync(Guid userId);
    }
}
