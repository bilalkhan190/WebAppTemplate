namespace WebAppTemplate.Application.DTOs.Responses;

public sealed record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Phone,
    DateTime CreatedAt);
