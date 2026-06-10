using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs.Account;

namespace WebAppTemplate.Application.Services.Abstraction
{
    public interface IAccountService
    {
        Task<ServiceResult<LoginResponse>> SignInAsync(LoginRequst request);
    }
}
