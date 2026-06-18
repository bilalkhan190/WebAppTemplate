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
        CreateMap<User, UserResponse>();
        CreateMap<Role, RoleResponse>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName ?? string.Empty));
        CreateMap<UserRoles, UserRoleResponse>();
        CreateMap<User, UserProfileResponse>()
    .ForCtorParam(
        nameof(UserProfileResponse.Roles),
        opt => opt.MapFrom(
            src => string.Join(",",
                src.UserRoles.Select(x => x.Roles.RoleName))
        ));
    }
}
