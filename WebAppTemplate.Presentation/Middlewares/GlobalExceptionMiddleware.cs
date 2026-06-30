using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using WebAppTemplate.Application.Common.Exceptions;
using WebAppTemplate.Application.DTOs;

namespace WebAppTemplate.Presentation.Middlewares;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public GlobalExceptionMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
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

            var message = statusCode == StatusCodes.Status500InternalServerError &&
                          !string.Equals(_environment.EnvironmentName, Environments.Development, StringComparison.OrdinalIgnoreCase)
                ? "An unexpected error occurred."
                : ex.Message;

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object>(false, message, null);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
