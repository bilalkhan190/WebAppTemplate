using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs.Account;

namespace WebAppTemplate.Presentation.Validations
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Refresh token is required.")
            .MaximumLength(500)
            .WithMessage("Refresh token exceeds maximum length.");

        }
    }
}
