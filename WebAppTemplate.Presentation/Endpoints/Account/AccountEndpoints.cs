using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Presentation.Endpoints.Abstractions;
using WebAppTemplate.Presentation.Endpoints.Constants;
using WebAppTemplate.Presentation.Endpoints.Extensions;

namespace WebAppTemplate.Presentation.Endpoints.Account;

/// <summary>
/// Maps to <see cref="IAuthService"/> + account-related <see cref="IUserService"/> operations.
/// Routes: /api/Account/*
/// </summary>
public sealed class AccountEndpoints : IEndpoint
{
  public void Map(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup(ApiRoutes.Account.Base);

    group.MapValidatedPost<RegisterUserRequest>(
            "/register",
            "AccountRegister",
            async (RegisterUserRequest request, IUserService userService, CancellationToken ct) =>
                await userService.RegisterUserAsync(request).ToHttpResult())
        .AllowAnonymous();

    group.MapValidatedPost<LoginRequest>(
            "/sign-in",
            "AccountSignIn",
            async (LoginRequest request, IAuthService authService, CancellationToken ct) =>
                await authService.SignInAsync(request).ToHttpResult())
        .AllowAnonymous();

    group.MapValidatedPost<RefreshTokenRequest>(
            "/refresh-token",
            "AccountRefreshToken",
            async (RefreshTokenRequest request, IAuthService authService, CancellationToken ct) =>
                await authService.GetNewRefreshToken(request).ToHttpResult())
        .AllowAnonymous();

    group.MapValidatedPost<RefreshTokenRequest>(
            "/logout",
            "AccountLogout",
            async (RefreshTokenRequest request, IAuthService authService, CancellationToken ct) =>
                await authService.LogoutAsync(request).ToHttpResult())
        .RequireAuth();

    group.MapGet(
            "/me",
            "AccountGetProfile",
            async (IUserService userService, CancellationToken ct) =>
                await userService.GetUserProfileAsync().ToHttpResult())
        .RequireAuth();

    group.MapValidatedPost<ChangePasswordRequest>(
            "/change-password",
            "AccountChangePassword",
            async (ChangePasswordRequest request, IAuthService authService, CancellationToken ct) =>
                await authService.ChangePasswordAsync(request).ToHttpResult())
        .RequireAuth();
  }
}
