using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
