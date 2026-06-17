using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.Common.Exceptions
{
    public sealed class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
            
        }
    }
}
