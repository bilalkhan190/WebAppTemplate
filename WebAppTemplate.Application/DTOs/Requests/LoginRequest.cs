namespace WebAppTemplate.Application.DTOs.Requests;

public sealed record LoginRequest(
    string Username,
    string Password);
