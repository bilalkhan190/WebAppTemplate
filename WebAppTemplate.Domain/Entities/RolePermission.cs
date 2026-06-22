using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        [Key]
        public Guid RolePermissionId { get; set; }
        public Guid RoleId { get; set; }    
        public Guid PermissionId { get; set; }

        public virtual Role Role { get; set; } = default!;
        public virtual Permission Permission { get; set; } = default!;
    }
}
