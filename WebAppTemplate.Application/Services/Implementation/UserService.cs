using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;

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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllUsers();
        }


       
        public async Task<User> RegisterUserAsync(RegisterUserRequestDTO Request)
        {
            var userToCreate = _mapper.Map<User>(Request);
            userToCreate.Password = _passwordManager.HashPassword(Request.Password);
            userToCreate.SetCreatedDefaults(userToCreate.CreatedBy.Value); 
            var result = await _unitOfWork.Users.RegisterAsync(userToCreate);
            await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}
