using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.DTOs
{
    public record RegisterUserRequestDTO
    {
        public Guid Id { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }


        public RegisterUserRequestDTO() { }

        public RegisterUserRequestDTO(Guid id, string username, string password, string email, string phoneNumber)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        
         
    }

   
}
