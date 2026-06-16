namespace WebAppTemplate.Application.DTOs.Responses;

public sealed record RoleResponse(
    Guid Id,
    string RoleName,
    DateTime CreatedAt);
