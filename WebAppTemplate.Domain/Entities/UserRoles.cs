using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Domain.Entities
{
    public class UserRoles
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } 
        public Guid RoleId { get; set; } 

        public virtual User Users { get; set; }
        public virtual Role Roles { get; set; }
    }
}
