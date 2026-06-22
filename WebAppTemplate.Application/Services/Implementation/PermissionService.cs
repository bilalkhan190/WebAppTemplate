using AutoMapper;
using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Responses;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Enums;
using WebAppTemplate.Domain.Shared.Enums;

namespace WebAppTemplate.Application.Services.Implementation
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly CurrentUser _userObject;
        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _userObject = _currentUserService.GetCurrentUser();
        }

        public async Task<ServiceResult<PermissionResponse>> CreateAsync(CreatePermissionRequest request)
        {
            var permission = _mapper.Map<Permission>(request);
            await  _unitOfWork.Permissions.AddAsync(permission);
            await _unitOfWork.CompleteAsync();

            var response = new PermissionResponse(
                                     permission.PermissionId,
                                     permission.Code,
                                     permission.Name,
                                     permission.CreatedAt.ToString("dd/MM/yyyy"),
                                     _userObject.Username,
                                     permission.Active.ToString());

            return ServiceResult<PermissionResponse>.FromSuccess(response);
        }

        public async Task<ServiceResult<CreateRolePermissionResponse>> CreateRolePermissionsAsync(CreateRolePermissionRequest request)
        {
            var isExist = await _unitOfWork.RolePermissions.AnyAsync(x => x.RoleId == request.RoleId && x.Active == Status.Active);
            if (isExist)
                return ServiceResult<CreateRolePermissionResponse>
                        .FromFailure(["permissions already exist"]
                        , ErrorType.Validation);

           var rolePermissions = _mapper.Map<List<RolePermission>>(request);
           await _unitOfWork.RolePermissions.CreateRolePermissionAsync(rolePermissions);
           await _unitOfWork.CompleteAsync();
            var response = new CreateRolePermissionResponse(
                                 rolePermissions.First().RoleId,
                                 rolePermissions.ToDictionary(
                                     x => x.PermissionId,
                                     x => x.Permission.Name)
                             );

            return ServiceResult<CreateRolePermissionResponse>.FromSuccess(response);
        }
    }
}
