namespace WebAppTemplate.Application.DTOs.Responses;

public sealed record UserRoleResponse(
    Guid Id,
    Guid UserId,
    Guid RoleId,
    DateTime CreatedAt);
