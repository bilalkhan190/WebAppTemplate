using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.Common.Results
{
    public sealed class CurrentUser
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string[] Roles { get; set; } = [];
    }
}
