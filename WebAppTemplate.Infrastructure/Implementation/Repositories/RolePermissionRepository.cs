using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Abstraction;
using WebAppTemplate.Domain.Entities;
using WebAppTemplate.Infrastructure.Persistance.Data;

namespace WebAppTemplate.Infrastructure.Implementation.Repositories
{
    internal class RolePermissionRepository : Repository<RolePermission>, IUserRolePermissionRepository
    {
        public RolePermissionRepository(ApplicationDbContext context) : base(context) { }
        

        public async Task<IEnumerable<RolePermission>> CreateRolePermissionAsync(List<RolePermission> rolePermission)
        {
           await AddRangeAsync(rolePermission);
           return rolePermission;
        }

        
    }
}
