using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Responses;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Application.Services.Abstraction;

public interface IAuthService
{
    Task<ServiceResult<LoginResponse>> SignInAsync(LoginRequest request);
    Task<RefreshToken?> GetTokenAsync(string refreshToken);
    Task<ServiceResult<LoginResponse>> GetNewRefreshToken(RefreshTokenRequest request);
}
