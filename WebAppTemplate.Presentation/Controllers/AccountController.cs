using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs;
using WebAppTemplate.Application.DTOs.Account;
using WebAppTemplate.Application.Extensions;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Application.Services.Implementation;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Presentation.Extensions;
using static WebAppTemplate.Application.Extensions.ApiExtension;

namespace WebAppTemplate.Presentation.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _userService.GetAllUsersAsync();

            return result.ToActionResult();
        }



        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(
                        [FromBody] RegisterUserRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _userService.RegisterUserAsync(request);

            return result.ToActionResult<User>();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(LoginRequst request) 
        {
            var result = await _authService.SignInAsync(request);
            return result.ToActionResult<LoginResponse>();
         }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request) {
            var result = await _authService.GetNewRefreshToken(request);
            return result.ToActionResult<LoginResponse>();
        } 


    }
}
