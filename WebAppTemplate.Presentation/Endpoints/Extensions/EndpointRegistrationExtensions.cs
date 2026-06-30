using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using WebAppTemplate.Presentation.Endpoints.Abstractions;

namespace WebAppTemplate.Presentation.Endpoints.Extensions;

public static class EndpointRegistrationExtensions
{
  public static IServiceCollection AddEndpoints(this IServiceCollection services)
  {
    var endpointTypes = Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(type => type is { IsAbstract: false, IsInterface: false }
                       && typeof(IEndpoint).IsAssignableFrom(type));

    foreach (var type in endpointTypes)
      services.AddSingleton(typeof(IEndpoint), type);

    return services;
  }

  public static IEndpointRouteBuilder MapEndpoints(this WebApplication app)
  {
    var endpoints = app.Services.GetServices<IEndpoint>();
    foreach (var endpoint in endpoints)
      endpoint.Map(app);

    return app;
  }
}
