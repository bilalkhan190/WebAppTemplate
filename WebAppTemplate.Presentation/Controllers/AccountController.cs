using Microsoft.AspNetCore.Mvc;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Responses;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Presentation.Extensions;

namespace WebAppTemplate.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AccountController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

   
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
    {
        var result = await _userService.RegisterUserAsync(request);
        return result.ToActionResult();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
    {
        var result = await _authService.SignInAsync(request);
        return result.ToActionResult();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.GetNewRefreshToken(request);
        return result.ToActionResult();
    }
}
