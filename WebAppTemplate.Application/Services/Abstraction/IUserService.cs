using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Responses;

namespace WebAppTemplate.Application.Services.Abstraction;

public interface IUserService
{
    Task<ServiceResult<IEnumerable<UserResponse>>> GetAllUsersAsync();
    Task<ServiceResult<UserResponse>> RegisterUserAsync(RegisterUserRequest request);
    Task<ServiceResult<UserRoleResponse>> AssignUserRole(CreateRoleAssignmentRequest request);
    Task<ServiceResult<RoleResponse>> CreateRoleAsync(CreateRoleRequest request);
}
