using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Infrastructure.Implementation.Repositories;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.Infrastructure.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbcontext;
        private IDbContextTransaction _transaction;
        private IUserRepository? _users;
        private ITokenRepository _refreshToken;
        private IUserRoleRepository _userRole;
        private IRepository<Role> _roles;
        private IUserRolePermissionRepository _rolePermission;
        private IRepository<Permission> _permission;

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public async Task BeginTransactionAsync() => _transaction = await _dbcontext.Database.BeginTransactionAsync();

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

        public int Complete() => _dbcontext.SaveChanges();

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

        public IUserRepository Users
     => _users ??= new UserRepository(_dbcontext);

        public ITokenRepository RefreshToken
            => _refreshToken ??= new TokenRepository(_dbcontext);

        public IUserRoleRepository UserRoles 
            => _userRole ??= new UserRoleRepository(_dbcontext);

        public IRepository<Role> Roles
            => _roles ??= new Repository<Role>(_dbcontext);

        public IUserRolePermissionRepository RolePermissions => _rolePermission ??= new RolePermissionRepository(_dbcontext);

        public IRepository<Permission> Permissions => _permission ??= new PermissionRepository(_dbcontext);
    }
}
