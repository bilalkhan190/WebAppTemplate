using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Shared.Constants;
using WebAppTemplate.Infrastructure.Authentication;
using WebAppTemplate.Infrastructure.Authentication.Settings;
using WebAppTemplate.Infrastructure.Implementation;
using WebAppTemplate.Infrastructure.Implementation.Repositories;
using WebAppTemplate.Infrastructure.Implementation.Services;
using WebAppTemplate.Infrastructure.Persistance.Data;
using WebAppTemplate.Infrastructure.Seeding;

namespace WebAppTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services , IConfigurationManager configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection("JwtSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           



            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration
                                            .GetSection("JwtSettings")
                                            .Get<JWTSettings>()
                                            ?? throw new InvalidOperationException(
                                                "JwtSettings configuration missing.");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });
          
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(ConnectionNames.Local),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null));
            });
            services.AddHttpContextAccessor();
            services.AddHealthChecks()
          .AddSqlServer(
              configuration.GetConnectionString(ConnectionNames.Local));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUserRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IDataSeeder, DataSeeder>();
            return services;
        }
    }
}
