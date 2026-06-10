using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Domain.Enums;
using WebAppTemplate.Domain.Shared.Enums;

namespace WebAppTemplate.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IPasswordManager _passwordManager;
        public UserService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            IPasswordManager passwordManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordManager = passwordManager;
        }

        public async Task<ServiceResult<IEnumerable<User>>> GetAllUsersAsync()
        {
                    var list = await _unitOfWork.Users
                            .Query()
                            .Where(x => x.Active == Status.Active)
                            .ToListAsync();

           return ServiceResult<IEnumerable<User>>.FromSuccess(list);
        }



        public async Task<ServiceResult<User>> RegisterUserAsync(RegisterUserRequestDTO request)
        {
            var existingUser =
                await _unitOfWork.Users.Query()
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingUser is not null)
            {
                return ServiceResult<User>.FromFailure(
                    ["Email already exists"],
                    ErrorType.Conflict);
            }
            User userToCreate = _mapper.Map<User>(request);
            userToCreate.Password =
                _passwordManager.HashPassword(request.Password);
            userToCreate.SetCreatedDefaults(userToCreate.CreatedBy.Value);
            await _unitOfWork.Users.AddAsync(userToCreate);
            await _unitOfWork.CompleteAsync();

            return ServiceResult<User>.FromSuccess(userToCreate);
        }
    }
}
