using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Shared.Enums;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.IntegrationTests;

internal sealed class IntegrationTestSeedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public IntegrationTestSeedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        if (await context.Users.AnyAsync(cancellationToken))
        {
            return;
        }

        var passwordManager = scope.ServiceProvider.GetRequiredService<IPasswordManager>();
        var adminRole = new Role { RoleName = "Administrator" };
        adminRole.SetCreatedDefaults(null);

        var adminUser = new User
        {
            FirstName = "System",
            LastName = "Administrator",
            Username = "admin",
            Email = "admin@webapptemplate.local",
            Phone = "0000000000",
            Password = passwordManager.HashPassword("Admin@123"),
            Role = AppRoles.Admin
        };
        adminUser.SetCreatedDefaults(null);

        await context.Roles.AddAsync(adminRole, cancellationToken);
        await context.Users.AddAsync(adminUser, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var userRole = new UserRoles
        {
            UserId = adminUser.UserId,
            RoleId = adminRole.RoleId
        };
        userRole.SetCreatedDefaults(null);
        await context.UserRoles.AddAsync(userRole, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
