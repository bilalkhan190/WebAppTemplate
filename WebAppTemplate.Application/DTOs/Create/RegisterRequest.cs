using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.DTOs
{
    public record RegisterUserRequestDTO
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
       
        public string FirstName { get; init; }
        public string LastName { get; init; }


        public RegisterUserRequestDTO() { }

        public RegisterUserRequestDTO( string username, string password, string email, string phoneNumber, string firstName , string lastName)
        {
           
            Username = username;
            Password = password;
            Email = email;
            Phone = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
        }
        
         
    }

   
}
