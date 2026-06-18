using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.DTOs.Responses
{
    public sealed record UserProfileResponse(
     Guid Id,
     string FirstName,
     string LastName,
     string Username,
     string Email,
     string Phone,
     DateTime CreatedAt,
     string Roles
     );

}
