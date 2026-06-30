using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("IntegrationTests_Shared"));

            services.AddHostedService<IntegrationTestSeedService>();
        });
    }
}
