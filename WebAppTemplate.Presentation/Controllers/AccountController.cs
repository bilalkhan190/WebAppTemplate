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
using WebAppTemplate.Domain.Entities;

using static WebAppTemplate.Application.Extensions.ApiExtension;

namespace WebAppTemplate.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;

        public AccountController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
           var users = await _serviceUnitOfWork.User.GetAllUsersAsync();

            if (users is null)
            {
                return Failure("avc",default);
            }
           
            return Success(users);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync ([FromBody]RegisterUserRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return InternalServerError("Validation Errors", ModelState);
            }

            var user = await _serviceUnitOfWork.User.RegisterUserAsync(request);

            if (user is null)
            {
                return Failure("Unable to register user", default);
            }
            //shoot an notification
            return Success(user,"user created successfull");
         
        }

      
    }
}
