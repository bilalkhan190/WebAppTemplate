using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Services.Abstraction;
using WebAppTemplate.Domain.Abstraction;

namespace WebAppTemplate.Application.Services.Implementation
{
    public class ServiceUnitOfWork : IServiceUnitOfWork , IDisposable
    {
        private bool _disposed; // Use a different variable name to avoid confusion
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordManager _passwordManager;
        private readonly IMapper _mapper;

        public ServiceUnitOfWork(IUnitOfWork unitOfWork, IPasswordManager passwordManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _passwordManager = passwordManager ?? throw new ArgumentNullException(nameof(passwordManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            User = new UserService(_unitOfWork, _mapper,_passwordManager);
        }
        public IUserService User { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
