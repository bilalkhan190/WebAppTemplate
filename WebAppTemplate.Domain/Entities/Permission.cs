using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public Guid PermissionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
      = new List<RolePermission>();

    }
}
