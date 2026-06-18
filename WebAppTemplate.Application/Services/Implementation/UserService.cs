using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebAppTemplate.Application.Common.Exceptions;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Requests.GET;
using WebAppTemplate.Application.DTOs.Responses;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Enums;
using WebAppTemplate.Domain.Shared.Enums;
using WebAppTemplate.Domain.Shared.Models;
using WebAppTemplate.Infrastructure.Extensions;
namespace WebAppTemplate.Application.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRoleRepository _userRoleRepo;
    private readonly IMapper _mapper;
    private readonly IPasswordManager _passwordManager;
    private readonly CurrentUser _currentUserObject;
    private readonly ICurrentUserService _currentUserService ;
    public UserService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordManager passwordManager,
        ICurrentUserService currentUserService,
        IUserRoleRepository userRoleRepo
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordManager = passwordManager;
        _currentUserService = currentUserService;
        _userRoleRepo = userRoleRepo;
        _currentUserObject = _currentUserService.GetCurrentUser();
    }

    public async Task<ServiceResult<UserRoleResponse>> AssignUserRole(CreateRoleAssignmentRequest request)
    {
        var userRole = new UserRoles
        {
            RoleId = request.RoleId,
            UserId = request.UserId
        };

        await _unitOfWork.UserRoles.AddAsync(userRole);
        await _unitOfWork.CompleteAsync();

        return ServiceResult<UserRoleResponse>.FromSuccess(_mapper.Map<UserRoleResponse>(userRole));
    }

    public async Task<ServiceResult<RoleResponse>> CreateRoleAsync(CreateRoleRequest request)
    {
        var role = _mapper.Map<Role>(request);
        await _unitOfWork.Roles.AddAsync(role);
        await _unitOfWork.CompleteAsync();

        return ServiceResult<RoleResponse>.FromSuccess(_mapper.Map<RoleResponse>(role));
    }

    public async Task<ServiceResult<UserProfileResponse>> GetUserProfileAsync()
    {
        var user = await _unitOfWork.Users.Query()
                                         .Include(x => x.UserRoles)
                                             .ThenInclude(x => x.Roles)
                                         .FirstOrDefaultAsync();
        if (user is null)
            throw new NotFoundException($"User not exist");
        var response = _mapper.Map<UserProfileResponse>(user);

        return ServiceResult<UserProfileResponse>.FromSuccess(response);
    }

    public async Task<ServiceResult<IEnumerable<UserResponse>>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.Users
            .Query()
            .Where(x => x.Active == Status.Active)
            .ToListAsync();

        return ServiceResult<IEnumerable<UserResponse>>.FromSuccess(
            _mapper.Map<IEnumerable<UserResponse>>(users));
    }

    public async Task<ServiceResult<PaginatedList<UserResponse>>> GetAllUsersAsync(GetUserRequest request)
    {
        var users = await _unitOfWork.Users
                                 .Query()
                                 .Where(x => x.Active == Status.Active)
                                 .ProjectTo<UserResponse>(
                                     _mapper.ConfigurationProvider)
                                 .ToPaginatedListAsync(
                                     request.PageNumber,
                                     request.PageSize);

                                    return ServiceResult<PaginatedList<UserResponse>>
                                        .FromSuccess(users);
    }

    public async Task<ServiceResult<UserResponse>> RegisterUserAsync(RegisterUserRequest request)
    {
        var existingUser = await _unitOfWork.Users
            .Query()
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (existingUser is not null)
        {
            return ServiceResult<UserResponse>.FromFailure(
                ["Email already exists"],
                ErrorType.Conflict);
        }

        var userToCreate = _mapper.Map<User>(request);
        userToCreate.Password = _passwordManager.HashPassword(request.Password);

        await _unitOfWork.Users.AddAsync(userToCreate);
        await _unitOfWork.CompleteAsync();

        return ServiceResult<UserResponse>.FromSuccess(_mapper.Map<UserResponse>(userToCreate));
    }
}
