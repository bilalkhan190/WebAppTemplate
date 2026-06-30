using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.Mapper;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Application.Services.Implementation;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Shared.Enums;
using WebAppTemplate.Infrastructure.Implementation;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.Application.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task RegisterUserAsync_ReturnsConflict_WhenEmailExists()
    {
        var context = CreateInMemoryContext();
        var existingUser = CreateUser("existing@example.com", "existing");
        context.Users.Add(existingUser);
        await context.SaveChangesAsync();

        var service = CreateUserService(context);

        var result = await service.RegisterUserAsync(new RegisterUserRequest(
            "newuser",
            "Password@123",
            "existing@example.com",
            "1234567890",
            "New",
            "User"));

        result.Should().BeOfType<ServiceResult<Application.DTOs.Responses.UserResponse>.Failure>();
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsNotFound_WhenUserMissing()
    {
        var context = CreateInMemoryContext();
        var service = CreateUserService(context);

        var result = await service.GetUserByIdAsync(Guid.NewGuid());

        result.Should().BeOfType<ServiceResult<Application.DTOs.Responses.UserResponse>.Failure>();
    }

    [Fact]
    public async Task DeleteUserAsync_SoftDeletesUser()
    {
        var context = CreateInMemoryContext();
        var user = CreateUser("delete@example.com", "delete-me");
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var service = CreateUserService(context);

        var result = await service.DeleteUserAsync(user.UserId);

        result.Should().BeOfType<ServiceResult<string>.Success>();
        var deletedUser = await context.Users.FirstAsync(x => x.UserId == user.UserId);
        deletedUser.Active.Should().Be(Status.InActive);
        deletedUser.Deleted.Should().Be(DeletionStatus.Deleted);
    }

    private static UserService CreateUserService(ApplicationDbContext context)
    {
        var unitOfWork = new UnitOfWork(context);
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperConfig>()).CreateMapper();
        var passwordManager = new PasswordManager();
        var currentUserService = new Mock<ICurrentUserService>();
        currentUserService
            .Setup(x => x.GetCurrentUser())
            .Returns(new CurrentUser { UserId = Guid.NewGuid(), Username = "tester" });

        return new UserService(
            unitOfWork,
            mapper,
            passwordManager,
            currentUserService.Object,
            unitOfWork.UserRoles);
    }

    private static User CreateUser(string email, string username)
    {
        var user = new User
        {
            FirstName = "Test",
            LastName = "User",
            Username = username,
            Email = email,
            Phone = "1234567890",
            Password = "hashed",
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
}
