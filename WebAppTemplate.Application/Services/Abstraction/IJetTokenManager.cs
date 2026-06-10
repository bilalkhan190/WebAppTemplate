using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Entities;


namespace WebAppTemplate.Application.Services.Abstraction
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
