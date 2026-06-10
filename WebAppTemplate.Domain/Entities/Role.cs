using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Domain.Entities
{
    public class Role : BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();  
        public string? RoleName { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }

    }
}
