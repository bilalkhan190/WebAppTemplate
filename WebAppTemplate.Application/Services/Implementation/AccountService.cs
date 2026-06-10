using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
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

namespace WebAppTemplate.Application.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordManager _passwordManager;

        public AccountService(IUnitOfWork unitOfWork, IPasswordManager passwordManager)
        {
            _unitOfWork = unitOfWork;
            _passwordManager = passwordManager;
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
            var isValidPassword = _passwordManager.VerifyPasswordAsync(
                                request.Password,
                                user.Password);
            if (isValidPassword) { }
            //will generate the jwt token

        }
    }
}
