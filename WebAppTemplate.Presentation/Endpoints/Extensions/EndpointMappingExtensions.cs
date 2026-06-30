using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WebAppTemplate.Presentation.Endpoints.Filters;

namespace WebAppTemplate.Presentation.Endpoints.Extensions;

/// <summary>
/// Shared route registration — validation filter + naming, bina handler repeat kiye.
/// </summary>
public static class EndpointMappingExtensions
{
  public static RouteHandlerBuilder MapValidatedPost<TRequest>(
      this RouteGroupBuilder group,
      string pattern,
      string endpointName,
      Delegate handler)
  {
    return group.MapPost(pattern, handler)
        .WithName(endpointName)
        .AddEndpointFilter<ValidationEndpointFilter<TRequest>>();
  }

  public static RouteHandlerBuilder MapValidatedGet<TRequest>(
      this RouteGroupBuilder group,
      string pattern,
      string endpointName,
      Delegate handler)
      where TRequest : notnull
  {
    return group.MapGet(pattern, handler)
        .WithName(endpointName)
        .AddEndpointFilter<ValidationEndpointFilter<TRequest>>();
  }

  public static RouteHandlerBuilder MapValidatedPut<TRequest>(
      this RouteGroupBuilder group,
      string pattern,
      string endpointName,
      Delegate handler)
  {
    return group.MapPut(pattern, handler)
        .WithName(endpointName)
        .AddEndpointFilter<ValidationEndpointFilter<TRequest>>();
  }

  public static RouteHandlerBuilder MapGet(
      this RouteGroupBuilder group,
      string pattern,
      string endpointName,
      Delegate handler)
  {
    return group.MapGet(pattern, handler)
        .WithName(endpointName);
  }

  public static RouteHandlerBuilder MapDelete(
      this RouteGroupBuilder group,
      string pattern,
      string endpointName,
      Delegate handler)
  {
    return group.MapDelete(pattern, handler)
        .WithName(endpointName);
  }

  public static RouteHandlerBuilder RequireAuth(this RouteHandlerBuilder builder)
      => builder.RequireAuthorization();

  public static RouteHandlerBuilder RequireRole(
      this RouteHandlerBuilder builder,
      string role)
      => builder.RequireAuthorization(new AuthorizeAttribute { Roles = role });
}
