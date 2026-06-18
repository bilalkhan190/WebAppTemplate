using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Requests.GET;
using WebAppTemplate.Application.DTOs.Responses;
using WebAppTemplate.Domain.Shared.Models;

namespace WebAppTemplate.Application.Services.Abstraction;

public interface IUserService
{
    Task<ServiceResult<PaginatedList<UserResponse>>> GetAllUsersAsync(GetUserRequest request);
    Task<ServiceResult<UserResponse>> RegisterUserAsync(RegisterUserRequest request);
    Task<ServiceResult<UserRoleResponse>> AssignUserRole(CreateRoleAssignmentRequest request);
    Task<ServiceResult<UserProfileResponse>> GetUserProfileAsync();
    Task<ServiceResult<RoleResponse>> CreateRoleAsync(CreateRoleRequest request);
}
