using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Shared.Models;

namespace WebAppTemplate.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageNumber , int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query
                               .Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();
            return new PaginatedList<T>(
                                items,
                                count,
                                pageNumber,
                                pageSize);
        }
    }
}
