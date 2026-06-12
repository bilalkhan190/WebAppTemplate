using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.DTOs.Create
{
    public sealed record CreateRoleAssignment(
       Guid RoleId,
       Guid UserId
             );


    public sealed record CreateRole(string RoleName);
}
