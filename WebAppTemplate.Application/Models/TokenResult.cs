using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Infrastructure.Authentication.Results
{
    public sealed record TokenResult
    {
        public string Accesstoken { get; set; }
        public int ExpiredAt { get; set; }
    }
}
