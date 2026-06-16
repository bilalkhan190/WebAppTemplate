namespace WebAppTemplate.Application.DTOs.Requests;

public sealed record CreateRoleAssignmentRequest(
    Guid RoleId,
    Guid UserId);
