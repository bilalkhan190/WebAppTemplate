using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Domain.Enums
{
    public enum ErrorType
    {
        Validation,
        NotFound,
        Conflict,
        Database,
        Unexpected
    }
}
