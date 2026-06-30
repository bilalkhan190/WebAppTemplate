using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Infrastructure.Authentication.Results;
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

        public TokenResult GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.UserId.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.Username),

            new Claim(
                ClaimTypes.Email,
                user.Email)
        };
            if (user.UserRoles.Count > 0)
            {
                foreach (var userRole in user.UserRoles)
                {
                    claims.Add(
                        new Claim(
                            ClaimTypes.Role,
                            userRole.Roles.RoleName));

                    foreach (var rolePermission in userRole.Roles.RolePermissions)
                    {
                        if (rolePermission.Active == Domain.Shared.Enums.Status.Active &&
                            rolePermission.Permission is not null)
                        {
                            claims.Add(new Claim("permission", rolePermission.Permission.Code));
                        }
                    }
                }
            }
           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int exipiredAt = _settings.ExpirationMinutes;
            var token = new JwtSecurityToken(
                           issuer: _settings.Issuer,
                           audience: _settings.Audience,
                           claims: claims,
                           expires: DateTime.UtcNow.AddMinutes(exipiredAt), signingCredentials: credentials);

            string accessToken = new JwtSecurityTokenHandler()
            .WriteToken(token);
            return new TokenResult { Accesstoken = accessToken, ExpiredAt = exipiredAt };

        }


    }
}
