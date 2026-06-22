using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Entities;

namespace WebAppTemplate.Domain.Abstraction
{
    public interface IUserRolePermissionRepository : IRepository<RolePermission>
    {
        Task<IEnumerable<RolePermission>> CreateRolePermissionAsync(List<RolePermission> rolePermission);
    }
}
