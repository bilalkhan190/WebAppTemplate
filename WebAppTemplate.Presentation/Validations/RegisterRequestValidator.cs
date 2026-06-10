using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs;

namespace WebAppTemplate.Presentation.Validations
{
    public  class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequestDTO>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(request => request.Username)
                .NotEmpty().WithMessage("username is a required")
                .Length(3, 20).WithMessage("Username must be between 3 and 20 characters.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Username can only contain alphanumeric characters.");
                //.MustAsync(BeUniqueUsername).WithMessage("Username is already taken.");

            RuleFor(request => request.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("Invalid email format.");
           //.MustAsync(BeUniqueEmail).WithMessage("Email is already registered.");

            RuleFor(request => request.Phone)
            .NotEmpty().WithMessage("Phone number is required.");
            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Password is required.")

       .MinimumLength(8)
       .WithMessage("Password must be at least 8 characters long.")

       .Matches("[A-Z]")
       .WithMessage("Password must contain at least one uppercase letter.")

       .Matches("[a-z]")
       .WithMessage("Password must contain at least one lowercase letter.")

       .Matches("[0-9]")
       .WithMessage("Password must contain at least one number.")

       .Matches("[^a-zA-Z0-9]")
       .WithMessage("Password must contain at least one special character.");
        }

        public async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            //will add validation logic here
            return await Task.FromResult(true);
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            
            return await Task.FromResult(true); 
        }
    }
}
