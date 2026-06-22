using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.DTOs.Requests
{
    public sealed record CreatePermissionRequest(string permissionName, string permissionCode);
    
}
