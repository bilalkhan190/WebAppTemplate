using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Mapper;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Application.Services.Implementation;

namespace WebAppTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfig));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
