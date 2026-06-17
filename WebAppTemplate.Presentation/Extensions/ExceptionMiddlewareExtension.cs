using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Presentation.Middlewares;

namespace WebAppTemplate.Presentation.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder
         UseGlobalExceptionMiddleware(
             this IApplicationBuilder app)
        {
            return app.UseMiddleware<
                GlobalExceptionMiddleware>();
        }
    }
}
