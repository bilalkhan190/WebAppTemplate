using AutoMapper;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Responses;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Application.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<CreateRoleRequest, Role>();
        CreateMap<CreatePermissionRequest, Permission>().ConstructUsing(p => new Permission
        {
            Code = p.permissionCode,
            Name = p.permissionName,
        });
        CreateMap<CreateRolePermissionRequest, List<RolePermission>>()
                                 .ConvertUsing(src =>
                                     src.PermissionId.Select(permissionId => new RolePermission
                                     {
                                         RoleId = src.RoleId,
                                         PermissionId = permissionId
                                     }).ToList());
                                CreateMap<User, UserResponse>();
                                CreateMap<Role, RoleResponse>()
                                    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName ?? string.Empty));
                                CreateMap<UserRoles, UserRoleResponse>();
                                CreateMap<User, UserProfileResponse>()
                                .ConvertUsing(user => new UserProfileResponse(
                                    user.UserId,
                                    user.FirstName,
                                    user.LastName,
                                    user.Username,
                                    user.Email,
                                    user.Phone,
                                    user.CreatedAt,
                                    string.Join(",", user.UserRoles.Select(ur => ur.Roles.RoleName))));
    }
}
