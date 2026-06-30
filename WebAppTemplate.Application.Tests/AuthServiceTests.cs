using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Responses;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Application.Services.Implementation;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Shared.Enums;
using WebAppTemplate.Infrastructure.Authentication.Results;
using WebAppTemplate.Infrastructure.Implementation;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.Application.Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task SignInAsync_ReturnsFailure_WhenUserNotFound()
    {
        var context = CreateInMemoryContext();
        var unitOfWork = CreateUnitOfWork(context);
        var authService = CreateAuthService(unitOfWork);

        var result = await authService.SignInAsync(new LoginRequest("missing", "Password@123"));

        result.Should().BeOfType<ServiceResult<LoginResponse>.Failure>();
    }

    [Fact]
    public async Task SignInAsync_ReturnsFailure_WhenPasswordInvalid()
    {
        var context = CreateInMemoryContext();
        var passwordManager = new PasswordManager();
        var user = CreateUser("john", passwordManager.HashPassword("Password@123"));
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var unitOfWork = CreateUnitOfWork(context);
        var authService = CreateAuthService(unitOfWork);

        var result = await authService.SignInAsync(new LoginRequest("john", "WrongPassword@1"));

        result.Should().BeOfType<ServiceResult<LoginResponse>.Failure>();
    }

    [Fact]
    public async Task SignInAsync_ReturnsTokens_WhenCredentialsValid()
    {
        var context = CreateInMemoryContext();
        var passwordManager = new PasswordManager();
        var user = CreateUser("john", passwordManager.HashPassword("Password@123"));
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var unitOfWork = CreateUnitOfWork(context);
        var authService = CreateAuthService(unitOfWork);

        var result = await authService.SignInAsync(new LoginRequest("john", "Password@123"));

        var success = result.Should().BeOfType<ServiceResult<LoginResponse>.Success>().Subject;
        success.Entity.AccessToken.Should().NotBeNullOrWhiteSpace();
        success.Entity.RefreshToken.Should().NotBeNullOrWhiteSpace();
    }

    private static AuthService CreateAuthService(Domain.Abstraction.IUnitOfWork unitOfWork)
    {
        var passwordManager = new PasswordManager();
        var tokenGenerator = new Mock<IJwtTokenGenerator>();
        tokenGenerator
            .Setup(x => x.GenerateToken(It.IsAny<User>()))
            .Returns(new TokenResult
            {
                Accesstoken = "test-access-token",
                ExpiredAt = 60
            });
        tokenGenerator
            .Setup(x => x.GenerateRefreshToken())
            .Returns("test-refresh-token");

        var currentUserService = new Mock<ICurrentUserService>();
        return new AuthService(unitOfWork, passwordManager, tokenGenerator.Object, currentUserService.Object);
    }

    private static User CreateUser(string username, string hashedPassword)
    {
        var user = new User
        {
            FirstName = "John",
            LastName = "Doe",
            Username = username,
            Email = $"{username}@example.com",
            Phone = "1234567890",
            Password = hashedPassword,
            Role = AppRoles.Visitor
        };
        user.SetCreatedDefaults(null);
        return user;
    }

    private static ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    private static Domain.Abstraction.IUnitOfWork CreateUnitOfWork(ApplicationDbContext context)
    {
        return new UnitOfWork(context);
    }
}
