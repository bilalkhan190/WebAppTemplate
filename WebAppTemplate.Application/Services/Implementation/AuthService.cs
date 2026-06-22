using Microsoft.EntityFrameworkCore;
using WebAppTemplate.Application.Common.Exceptions;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Responses;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Enums;

namespace WebAppTemplate.Application.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordManager _passwordManager;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly ICurrentUserService _currentUserService;

    public AuthService(
        IUnitOfWork unitOfWork,
        IPasswordManager passwordManager,
        IJwtTokenGenerator tokenGenerator,
        ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _passwordManager = passwordManager;
        _tokenGenerator = tokenGenerator;
        _currentUserService = currentUserService;
    }

    public async Task<RefreshToken?> GetTokenAsync(string refreshToken)
    {
        return await _unitOfWork.RefreshToken
            .Query()
            .FirstOrDefaultAsync(x =>
                x.Token == refreshToken &&
                x.RevokedAt == null &&
                x.ExpiresAt > DateTime.UtcNow);
    }

    private async Task<LoginResponse> GetAccessTokenAsync(User user)
    {
        var tokenResult = _tokenGenerator.GenerateToken(user);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        var expiresAt = DateTime.UtcNow.AddDays(1);

        await _unitOfWork.RefreshToken.AddAsync(new RefreshToken
        {
            UserId = user.UserId,
            Token = refreshToken,
            ExpiresAt = expiresAt,
        });

        return new LoginResponse(
            tokenResult.Accesstoken,
            refreshToken,
            expiresAt);
    }

    public async Task<ServiceResult<LoginResponse>> SignInAsync(LoginRequest request)
    {
        var user = await _unitOfWork.Users
            .Query()
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Username == request.Username);

        if (user is null)
        {
            return ServiceResult<LoginResponse>.FromFailure(
                ["Invalid username or password"],
                ErrorType.Validation);
        }

        var isValidPassword = _passwordManager.VerifyPassword(request.Password, user.Password);
        if (!isValidPassword)
        {
            return ServiceResult<LoginResponse>.FromFailure(
                ["Invalid username or password"],
                ErrorType.Validation);
        }

        var response = await GetAccessTokenAsync(user);
        await _unitOfWork.CompleteAsync();

        return ServiceResult<LoginResponse>.FromSuccess(response);
    }

    public async Task<ServiceResult<LoginResponse>> GetNewRefreshToken(RefreshTokenRequest request)
    {
        var existingToken = await GetTokenAsync(request.Token);
        if (existingToken is null)
        {
            return ServiceResult<LoginResponse>.FromFailure(
                ["Invalid or expired token"],
                ErrorType.Validation);
        }

        var user = await _unitOfWork.Users.GetByIdAsync(existingToken.UserId);
        await _unitOfWork.RefreshToken.RevokeAsync(request.Token);

        var newTokens = await GetAccessTokenAsync(user);
        await _unitOfWork.CompleteAsync();

        return ServiceResult<LoginResponse>.FromSuccess(newTokens);
    }

    public async Task<ServiceResult<string>> LogoutAsync(RefreshTokenRequest request)
    {
        var currentUser = _currentUserService.GetCurrentUser();
        if (currentUser is null)
        {
            return ServiceResult<string>.FromFailure(
                ["Unauthorized"],
                ErrorType.Validation);
        }

        var existingToken = await _unitOfWork.RefreshToken
            .Query()
            .FirstOrDefaultAsync(x =>
                x.Token == request.Token &&
                x.UserId == currentUser.UserId &&
                x.RevokedAt == null &&
                x.ExpiresAt > DateTime.UtcNow);

        if (existingToken is null)
        {
            return ServiceResult<string>.FromFailure(
                ["Invalid or expired token"],
                ErrorType.Validation);
        }

        await _unitOfWork.RefreshToken.RevokeAsync(request.Token);
        await _unitOfWork.CompleteAsync();

        return ServiceResult<string>.FromSuccess("Logged out successfully");
    }

    public async Task<ServiceResult<string>> ChangePasswordAsync(ChangePasswordRequest request)
    {
        var currentUser = _currentUserService.GetCurrentUser();
        if (currentUser is null)
        {
            return ServiceResult<string>.FromFailure(
                ["Unauthorized"],
                ErrorType.Validation);
        }

        var user = await _unitOfWork.Users.GetByIdAsync(currentUser.UserId);
        if (user is null)
            throw new NotFoundException("user not found");

        var isValidPassword = _passwordManager.VerifyPassword(
            request.CurrentPassword,
            user.Password);

        if (!isValidPassword)
        {
            return ServiceResult<string>.FromFailure(
                ["Current password is incorrect"],
                ErrorType.Validation);
        }

        user.Password = _passwordManager.HashPassword(request.NewPassword);
        await _unitOfWork.CompleteAsync();

        return ServiceResult<string>.FromSuccess("Password changed successfully");
    }
}
