using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Shared.Enums;

namespace WebAppTemplate.Domain.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }    
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public AppRoles Role { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }


    }
}
