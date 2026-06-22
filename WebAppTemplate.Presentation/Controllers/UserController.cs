using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Requests.GET;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Presentation.Extensions;

namespace WebAppTemplate.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPermissionService _permissionService;

    public UserController(IUserService userService,IPermissionService permissionService)
    {
        _userService = userService;
        _permissionService = permissionService;
    }

    [HttpPost("create-role-assignment")]
    public async Task<IActionResult> CreateRoleAssignment([FromBody] CreateRoleAssignmentRequest request)
    {
        var result = await _userService.AssignUserRole(request);
        return result.ToActionResult();
    }

    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
    {
        var result = await _userService.CreateRoleAsync(request);
        return result.ToActionResult();
    }

    [HttpGet("users")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetUsers([FromQuery] GetUserRequest request)
    {
        var result = await _userService.GetAllUsersAsync(request);
        return result.ToActionResult();
    }

    [HttpPost("create-role-permissions")]
    public async Task<IActionResult> CreateRolePermissions(CreateRolePermissionRequest request)
    {
        var result = await _permissionService.CreateRolePermissionsAsync(request);
        return result.ToActionResult();
    }

    [HttpPost("create-permission")]
    public async Task<IActionResult> CreatePermission(CreatePermissionRequest request) { 
          var result = await _permissionService.CreateAsync(request);
        return result.ToActionResult();
    }
}
