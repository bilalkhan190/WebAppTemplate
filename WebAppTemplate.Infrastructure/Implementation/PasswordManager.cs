using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Services.Abstraction;

namespace WebAppTemplate.Infrastructure.Implementation
{
    public class PasswordManager : IPasswordManager
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(
     string password,
     string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(
                password,
                hashPassword);
        }
    }
}
