using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs.Create;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Presentation.Extensions;

namespace WebAppTemplate.Presentation.Controllers
{
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
        public async Task<IActionResult> createRoleAssignment(CreateRoleAssignment request)
        {
            var result = await _userService.AssignUserRole(request);
            return result.ToActionResult<UserRoles>();
        }
    }
}
