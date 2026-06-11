using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Infrastructure.Implementation.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public CurrentUser GetCurrentUser()
        {
            var principal = _httpContextAccessor?.HttpContext?.User;
            if (principal?.Identity?.IsAuthenticated != true)
            {
                return null;
            }
            return new CurrentUser
            {
                UserId = Guid.Parse(
                    principal.FindFirst(ClaimTypes.NameIdentifier)?.Value),

                Username = principal.FindFirst(ClaimTypes.Name)?.Value,

                Roles = principal.FindAll(ClaimTypes.Role)
                       .Select(x => x.Value)
                       .ToArray()
            };

        }
    }
}
