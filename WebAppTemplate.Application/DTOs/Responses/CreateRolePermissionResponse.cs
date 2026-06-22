using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.DTOs.Responses
{
    public sealed record CreateRolePermissionResponse(
        Guid RoleId,
        Dictionary<Guid,string> Permissions
        );
   
}
