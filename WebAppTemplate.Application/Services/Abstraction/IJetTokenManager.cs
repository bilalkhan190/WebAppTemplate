using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Infrastructure.Authentication.Results;


namespace WebAppTemplate.Application.Services.Abstraction
{
    public interface IJwtTokenGenerator
    {
        string GenerateRefreshToken();
        TokenResult GenerateToken(User user);

    }
}
