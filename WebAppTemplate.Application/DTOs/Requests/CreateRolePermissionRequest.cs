using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.DTOs.Requests
{
    public sealed record CreateRolePermissionRequest(
        Guid RoleId,
        List<Guid> PermissionId
        );
    
}
