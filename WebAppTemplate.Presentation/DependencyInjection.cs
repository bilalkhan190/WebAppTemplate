using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebAppTemplate.Application.Extensions;

namespace WebAppTemplate.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        //shaping the default validation message into our structured response model
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(entry => entry.Value?.Errors.Count > 0)
                    .SelectMany(entry => entry.Value!.Errors)
                    .Select(error =>
                        string.IsNullOrWhiteSpace(error.ErrorMessage)
                            ? "Validation failed."
                            : error.ErrorMessage)
                    .ToList();

                return ApiExtension.Failure(
                    string.Join(", ", errors),
                    StatusCodes.Status400BadRequest);
            };
        });

        return services;
    }
}
