using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Routing;

using WebAppTemplate.Application.DTOs.Requests;

using WebAppTemplate.Application.DTOs.Requests.GET;

using WebAppTemplate.Application.Services.Abstraction;

using WebAppTemplate.Presentation.Endpoints.Abstractions;

using WebAppTemplate.Presentation.Endpoints.Constants;

using WebAppTemplate.Presentation.Endpoints.Extensions;



namespace WebAppTemplate.Presentation.Endpoints.Users;



/// <summary>

/// User management operations.

/// Routes: /api/User/*

/// </summary>

public sealed class UserManagementEndpoints : IEndpoint

{

  public void Map(IEndpointRouteBuilder app)

  {

    var group = app.MapGroup(ApiRoutes.User.Base)

        .RequireAuthorization();



    group.MapValidatedGet<GetUserRequest>(

            "/users",

            "UserGetAll",

            async ([AsParameters] GetUserRequest request, IUserService userService, CancellationToken ct) =>

                await userService.GetAllUsersAsync(request).ToHttpResult())

        .RequireAuthorization(AuthorizationPolicies.UsersRead);



    group.MapGet(

            "/users/{id:guid}",

            "UserGetById",

            async (Guid id, IUserService userService, CancellationToken ct) =>

                await userService.GetUserByIdAsync(id).ToHttpResult())

        .RequireAuthorization(AuthorizationPolicies.UsersRead);



    group.MapValidatedPut<UpdateUserRequest>(

            "/users/{id:guid}",

            "UserUpdate",

            async (Guid id, UpdateUserRequest request, IUserService userService, CancellationToken ct) =>

                await userService.UpdateUserAsync(id, request).ToHttpResult())

        .RequireAuthorization(AuthorizationPolicies.UsersManage);



    group.MapDelete(

            "/users/{id:guid}",

            "UserDelete",

            async (Guid id, IUserService userService, CancellationToken ct) =>

                await userService.DeleteUserAsync(id).ToHttpResult())

        .RequireAuthorization(AuthorizationPolicies.UsersManage);

  }

}


