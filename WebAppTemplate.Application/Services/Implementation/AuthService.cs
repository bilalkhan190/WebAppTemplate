using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Account;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Enums;
using WebAppTemplate.Infrastructure.Authentication.Results;

namespace WebAppTemplate.Application.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordManager _passwordManager;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IUnitOfWork unitOfWork, IPasswordManager passwordManager, IJwtTokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordManager = passwordManager;
            _tokenGenerator = tokenGenerator;
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
            DateTime expireAt = DateTime.UtcNow.AddDays(1);
            await _unitOfWork.RefreshToken.AddAsync(new Domain.Entities.RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = expireAt,
            });
           return  new LoginResponse
            {
                AccessToken = tokenResult.Accesstoken,
                RefreshToken = refreshToken,
                ExpiresAt = expireAt,

            };
        }
        public async Task<ServiceResult<LoginResponse>> SignInAsync(LoginRequst request)
        {
           var user =  await _unitOfWork.Users
                                    .Query()
                                    .FirstOrDefaultAsync(x => x.Username == request.Username);
            if (user is null)
            {
                return ServiceResult<LoginResponse>.FromFailure(
                    ["Invalid username or password"],
                    ErrorType.Validation);
            }
            var isValidPassword = _passwordManager.VerifyPassword(
                                request.Password,
                                user.Password);
            if (isValidPassword) {
                var response = await GetAccessTokenAsync(user);
                        await _unitOfWork.CompleteAsync();
                return ServiceResult<LoginResponse>.FromSuccess(response);
            }

            return ServiceResult<LoginResponse>.FromFailure(["Invalid Username or Password"], ErrorType.Validation);
        }

        public async Task<ServiceResult<LoginResponse>> GetNewRefreshToken(RefreshTokenRequest request)
        {
            var existingToken = await GetTokenAsync(request.Token);
            if (existingToken is null)
                return ServiceResult<LoginResponse>.FromFailure(["invalid or expired token"], ErrorType.Validation);
            var user = await _unitOfWork.Users.GetByIdAsync(existingToken.UserId);
            await _unitOfWork.RefreshToken.RevokeAsync(request.Token);
            var newTokens = await GetAccessTokenAsync(user);
            await _unitOfWork.CompleteAsync();
            return ServiceResult<LoginResponse>.FromSuccess(newTokens);
            
        }
    }
}
