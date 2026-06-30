using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Responses;

namespace WebAppTemplate.Application.Services.Abstraction
{
    public interface IPermissionService
    {
        Task<ServiceResult<PermissionResponse>> CreateAsync(CreatePermissionRequest request);
        Task<ServiceResult<CreateRolePermissionResponse>> CreateRolePermissionsAsync(CreateRolePermissionRequest request);
        Task<ServiceResult<IEnumerable<PermissionResponse>>> GetAllAsync();
    }
}
