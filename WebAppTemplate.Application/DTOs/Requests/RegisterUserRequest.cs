namespace WebAppTemplate.Application.DTOs.Requests;

public sealed record RegisterUserRequest(
    string Username,
    string Password,
    string Email,
    string Phone,
    string FirstName,
    string LastName);
