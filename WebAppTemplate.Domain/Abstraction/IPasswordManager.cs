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
        bool VerifyPassword(
       string password,
       string hashPassword);
    }
}
