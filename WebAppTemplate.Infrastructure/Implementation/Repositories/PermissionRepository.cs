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
    internal sealed class PermissionRepository : Repository<Permission> , IPermissionRepository
    {
      
        public PermissionRepository(ApplicationDbContext context) : base(context) { }
    }
}
