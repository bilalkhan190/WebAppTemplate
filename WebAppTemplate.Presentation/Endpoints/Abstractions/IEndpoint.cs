using Microsoft.AspNetCore.Routing;

namespace WebAppTemplate.Presentation.Endpoints.Abstractions;

public interface IEndpoint
{
    void Map(IEndpointRouteBuilder app);
}
