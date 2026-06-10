using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Application.DTOs;

namespace WebAppTemplate.Application.Services.Abstraction
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> RegisterUserAsync(RegisterUserRequestDTO Request);
    }
}
