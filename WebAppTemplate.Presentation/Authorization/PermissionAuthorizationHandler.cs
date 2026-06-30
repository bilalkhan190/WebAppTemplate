using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebAppTemplate.Presentation.Endpoints.Constants;

namespace WebAppTemplate.Presentation.Authorization;

public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.IsInRole(RoleNames.Administrator))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (context.User.Claims.Any(c =>
                c.Type == "permission" &&
                string.Equals(c.Value, requirement.Permission, StringComparison.OrdinalIgnoreCase)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
