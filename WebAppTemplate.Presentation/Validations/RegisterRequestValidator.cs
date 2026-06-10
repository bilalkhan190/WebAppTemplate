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

            RuleFor(request => request.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.");
            //.Matches(@"^\+\d{1,3}\d{9,12}$").WithMessage("Phone number must be in the format +<country_code><number>."); // Example: +12345678901
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
