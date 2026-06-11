using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Account;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Application.Services.Abstraction
{
    public interface IAuthService
    {
        Task<ServiceResult<LoginResponse>> SignInAsync(LoginRequst request);
        Task<RefreshToken?> GetTokenAsync(string refreshToken);
        Task<ServiceResult<LoginResponse>> GetNewRefreshToken(RefreshTokenRequest request);

    }
}
