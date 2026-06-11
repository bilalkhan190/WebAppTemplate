using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs.Account;
using WebAppTemplate.Domain.Entities;

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

    public class AssignmentRoleToUser : AbstractValidator<UserRoles>
    {
        public AssignmentRoleToUser()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("Userid is required.");
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage("Roleid is required.");

        }
    }
}
