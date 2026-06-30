namespace WebAppTemplate.Application.DTOs.Requests;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Phone);
