using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Domain.Shared.Enums
{
    public enum DeletionStatus {
        Deleted = 1,
        NotDeleted = 0
    }

    public enum Status
    {
        InActive = 0,
        Active = 1,
    }

    public enum AppRoles
    {
        SuperAdmin = 0,
        Admin = 1,
        Visitor = 2
    }
}
