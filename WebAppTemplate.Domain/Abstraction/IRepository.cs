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
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(
                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteAsync(int id);
        Task<TEntity> FindAsync(object id);
        Task<int> CountAsync();

        Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> selector);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize,
                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetWithTrackingAsync(int id);

        Task<IEnumerable<TEntity>> GetAllWithoutTrackingAsync(Expression<Func<TEntity, bool>> predicate = null);
        Task<IEnumerable<TEntity>> GetAllWithoutTrackingAsync();

        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task<int> ExecuteRawSqlAsync(string sql, params object[] parameters);

        Task<TEntity> GetWithIncludesAsync(
                                         Expression<Func<TEntity, bool>> predicate,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(
                                            Expression<Func<TEntity, bool>> predicate = null,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> GetWithoutTrackingAsync(int id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}
