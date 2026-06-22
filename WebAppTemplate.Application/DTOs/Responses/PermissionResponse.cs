using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.DTOs.Responses
{
    public sealed record PermissionResponse(Guid permissionId,
             string permissionName,
             string permissionCode,
             string CreatedDate,
             string CreatedByUser,
             string status
        );
    
}
