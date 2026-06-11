using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Domain.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CompleteAsync();
        int Complete();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        IUserRepository Users { get; }
        ITokenRepository RefreshToken { get; }
    }
}
