using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.Services.Abstraction
{
    public interface IPasswordManager
    {
        string HashPassword(string password);
        Task<bool> VerifyPasswordAsync(string password, string hashPassword, CancellationToken cancellationToken);
    }
}
