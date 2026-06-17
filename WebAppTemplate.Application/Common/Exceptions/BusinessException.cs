using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.Common.Exceptions
{
    public sealed class BusinessException : BaseException
    {
        public BusinessException(string message) : base(message)
        {
            
        }
    }
}
