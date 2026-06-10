using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.Infrastructure.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbcontext;
        private IDbContextTransaction _transaction;


        public UnitOfWork(ApplicationDbContext context)
        {
            _dbcontext = context;   
        }

        public async Task BeginTransactionAsync() => await _dbcontext.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync()
        {
            try
            {
                await CompleteAsync();
                await _transaction.CommitAsync();
               
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public int Complete() =>  _dbcontext.SaveChanges();
       
        public async Task<int> CompleteAsync() => await _dbcontext.SaveChangesAsync();
       

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is not null)
            {
                _transaction?.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }

        public IUserRepository Users => new UserRepository(_dbcontext);
    }
}
