using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs;
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

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _userService.GetAllUsersAsync();

            return result.ToActionResult();
        }



        [HttpPost("Register")]
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

    }
}
