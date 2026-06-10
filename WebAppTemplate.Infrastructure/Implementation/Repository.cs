using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Shared.Models;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.Infrastructure.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
           await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
           return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync()
        {
           return await _dbSet.CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var record = await _dbSet.FindAsync(id);
            if (record is not null)
            {
                 _dbSet.Remove(record);
            }
        }

        public async Task<int> ExecuteRawSqlAsync(string sql, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlRawAsync(sql,parameters);
        }

        /// <summary>
        /// you can use findall method for getting filtered record as an list directly
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();    
        }

        public async Task<TEntity> FindAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return  _dbSet.FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
           return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _dbSet.Where(predicate);
        }

        /// <summary>
        /// returns Ienumrable record with entity eager loaded
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            // Apply include expressions if provided
            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await Task.FromResult(query);
        }

        public Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithoutTrackingAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public Task<IEnumerable<TEntity>> GetAllWithoutTrackingAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetWithoutTrackingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetWithTrackingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> MaxAsync(Expression<Func<TEntity, decimal>> selector)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
