using System;
using System.Threading.Tasks;

using AcidLabsTest.Service.Models.Requests;
using AcidLabsTest.Service.Models.Responses;

namespace AcidLabsTest.Service.ServiceContracts
{
    public interface IUsersService
    {
        Task AddUserAsync(AddUserRequest addUserRequest);
        Task<Guid> GetUserIdByEmailAsync(string userEmail);
        Task<GetUserResponse> GetUserAsync(Guid userId);
        Task UpdateUserAsync(Guid userId);
        Task DeleteUserAsync(Guid userId);
    }
}
