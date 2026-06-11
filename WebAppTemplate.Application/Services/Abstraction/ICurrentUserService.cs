using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;

namespace WebAppTemplate.Application.Services.Abstraction
{
    public interface ICurrentUserService
    {
        CurrentUser GetCurrentUser();
    }
}
