using Microsoft.EntityFrameworkCore;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Shared.Enums;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.Infrastructure.Seeding;

public class DataSeeder : IDataSeeder
{
    private const string AdministratorRoleName = "Administrator";
    private const string UserRoleName = "User";
    private const string AdminUsername = "admin";
    private const string AdminPassword = "Admin@123";
    private const string AdminEmail = "admin@webapptemplate.local";
    private const string AdminFirstName = "System";
    private const string AdminLastName = "Administrator";
    private const string AdminPhone = "0000000000";

    private readonly ApplicationDbContext _context;
    private readonly IPasswordManager _passwordManager;

    public DataSeeder(ApplicationDbContext context, IPasswordManager passwordManager)
    {
        _context = context;
        _passwordManager = passwordManager;
    }

    public async Task SeedAsync()
    {
        await SeedRolesAsync();
        await SeedAdminUserAsync();
        await _context.SaveChangesAsync();
        await SeedAdminRoleAssignmentAsync();
        await _context.SaveChangesAsync();
    }

    private async Task SeedRolesAsync()
    {
        if (await _context.Roles.AnyAsync(r => r.RoleName == AdministratorRoleName))
        {
            return;
        }

        var administratorRole = new Role { RoleName = AdministratorRoleName };
        administratorRole.SetCreatedDefaults(null);

        var userRole = new Role { RoleName = UserRoleName };
        userRole.SetCreatedDefaults(null);

        await _context.Roles.AddRangeAsync(administratorRole, userRole);
    }

    private async Task SeedAdminUserAsync()
    {
        if (await _context.Users.AnyAsync(u => u.Username == AdminUsername))
        {
            return;
        }

        var adminUser = new User
        {
            FirstName = AdminFirstName,
            LastName = AdminLastName,
            Username = AdminUsername,
            Email = AdminEmail,
            Phone = AdminPhone,
            Password = _passwordManager.HashPassword(AdminPassword),
            Role = AppRoles.Admin
        };

        adminUser.SetCreatedDefaults(null);
        await _context.Users.AddAsync(adminUser);
    }

    private async Task SeedAdminRoleAssignmentAsync()
    {
        var adminUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == AdminUsername);

        var administratorRole = await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleName == AdministratorRoleName);

        if (adminUser is null || administratorRole is null)
        {
            return;
        }

        var assignmentExists = await _context.UserRoles
            .AnyAsync(ur => ur.UserId == adminUser.UserId && ur.RoleId == administratorRole.RoleId);

        if (assignmentExists)
        {
            return;
        }

        var userRole = new UserRoles
        {
            UserId = adminUser.UserId,
            RoleId = administratorRole.RoleId
        };

        userRole.SetCreatedDefaults(null);
        await _context.UserRoles.AddAsync(userRole);
    }
}
