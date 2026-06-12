using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs;
using WebAppTemplate.Application.DTOs.Create;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Application.Services.Abstraction
{
    public interface IUserService
    {
        Task<ServiceResult<IEnumerable<User>>> GetAllUsersAsync();
         Task<ServiceResult<User>> RegisterUserAsync(RegisterUserRequestDTO request);
        Task<ServiceResult<UserRoles>> AssignUserRole(CreateRoleAssignment request);
        Task<ServiceResult<Role>> CreateRoleAsync(CreateRole role);
    }
}
