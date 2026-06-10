using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Infrastructure.Authentication.Settings;

namespace WebAppTemplate.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private JWTSettings _settings;

        public JwtTokenGenerator(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateRefreshToken()
        {
           var randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes);
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.Username),

            new Claim(
                ClaimTypes.Email,
                user.Email)
        };
            foreach (var role in user.UserRoles)
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        role.Roles.RoleName));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                           issuer: _settings.Issuer,
                           audience: _settings.Audience,
                           claims: claims,
                           expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes), signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
           .WriteToken(token);

        }
    }
}
