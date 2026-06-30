using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebAppTemplate.Application.Extensions;
using WebAppTemplate.Presentation.Authorization;
using WebAppTemplate.Presentation.Endpoints.Constants;
using WebAppTemplate.Presentation.Endpoints.Extensions;

namespace WebAppTemplate.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.AdminOnly, policy =>
                policy.RequireRole(RoleNames.Administrator));

            options.AddPolicy(AuthorizationPolicies.UsersRead, policy =>
                policy.AddRequirements(new PermissionRequirement(PermissionNames.UsersRead)));

            options.AddPolicy(AuthorizationPolicies.UsersManage, policy =>
                policy.AddRequirements(new PermissionRequirement(PermissionNames.UsersManage)));

            options.AddPolicy(AuthorizationPolicies.RolesManage, policy =>
                policy.AddRequirements(new PermissionRequirement(PermissionNames.RolesManage)));

            options.AddPolicy(AuthorizationPolicies.PermissionsManage, policy =>
                policy.AddRequirements(new PermissionRequirement(PermissionNames.PermissionsManage)));
        });

        services.AddEndpoints();

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
