using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Presentation.Endpoints.Abstractions;
using WebAppTemplate.Presentation.Endpoints.Constants;
using WebAppTemplate.Presentation.Endpoints.Extensions;

namespace WebAppTemplate.Presentation.Endpoints.Platform;

/// <summary>
/// Platform-level RBAC management — roles, permissions, and assignments.
/// Routes: /api/Platform/*
/// </summary>
public sealed class PlatformEndpoints : IEndpoint
{
    public void Map(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(ApiRoutes.Platform.Base)
            .RequireAuthorization();

        group.MapValidatedPost<CreateRoleRequest>(
                "/roles",
                "PlatformCreateRole",
                async (CreateRoleRequest request, IUserService userService, CancellationToken ct) =>
                    await userService.CreateRoleAsync(request).ToHttpResult())
            .RequireAuthorization(AuthorizationPolicies.RolesManage);

        group.MapGet(
                "/roles",
                "PlatformGetRoles",
                async (IUserService userService, CancellationToken ct) =>
                    await userService.GetAllRolesAsync().ToHttpResult())
            .RequireAuthorization(AuthorizationPolicies.RolesManage);

        group.MapValidatedPost<CreatePermissionRequest>(
                "/permissions",
                "PlatformCreatePermission",
                async (CreatePermissionRequest request, IPermissionService permissionService, CancellationToken ct) =>
                    await permissionService.CreateAsync(request).ToHttpResult())
            .RequireAuthorization(AuthorizationPolicies.PermissionsManage);

        group.MapGet(
                "/permissions",
                "PlatformGetPermissions",
                async (IPermissionService permissionService, CancellationToken ct) =>
                    await permissionService.GetAllAsync().ToHttpResult())
            .RequireAuthorization(AuthorizationPolicies.PermissionsManage);

        group.MapValidatedPost<CreateRolePermissionRequest>(
                "/role-permissions",
                "PlatformCreateRolePermissions",
                async (CreateRolePermissionRequest request, IPermissionService permissionService, CancellationToken ct) =>
                    await permissionService.CreateRolePermissionsAsync(request).ToHttpResult())
            .RequireAuthorization(AuthorizationPolicies.PermissionsManage);

        group.MapValidatedPost<CreateRoleAssignmentRequest>(
                "/user-roles",
                "PlatformAssignUserRole",
                async (CreateRoleAssignmentRequest request, IUserService userService, CancellationToken ct) =>
                    await userService.AssignUserRole(request).ToHttpResult())
            .RequireAuthorization(AuthorizationPolicies.RolesManage);
    }
}
