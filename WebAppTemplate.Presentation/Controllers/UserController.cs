using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Presentation.Extensions;

namespace WebAppTemplate.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
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
    public async Task<IActionResult> GetUsers()
    {
        var result = await _userService.GetAllUsersAsync();
        return result.ToActionResult();
    }
}
