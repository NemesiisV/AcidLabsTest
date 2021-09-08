using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AcidLabsService.Models.Requests;
using AcidLabsService.Models.Responses;

namespace AcidLabsService.ServiceContracts
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
