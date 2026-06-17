using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Exceptions;
using WebAppTemplate.Application.DTOs;
using WebAppTemplate.Application.Extensions;

namespace WebAppTemplate.Presentation.Middlewares
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    ConflictException => StatusCodes.Status409Conflict,
                    BusinessException => StatusCodes.Status400BadRequest,
                    ForbiddenException => StatusCodes.Status403Forbidden,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>(
                    false,
                    ex.Message,
                    null);
                var json = JsonSerializer.Serialize(response);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}
