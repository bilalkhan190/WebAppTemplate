using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Shared.Models;

namespace WebAppTemplate.Domain.Abstraction
{
    public interface IRepository<TEntity>
     where TEntity : class
    {
        IQueryable<TEntity> Query();

        Task<TEntity?> GetByIdAsync(Guid id);

        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(
            Expression<Func<TEntity, bool>>? predicate = null);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        Task<PaginatedList<TEntity>> GetPagedAsync(
    int pageNumber,
    int pageSize);
    }
}
