namespace WebAppTemplate.Application.DTOs.Responses;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt);
