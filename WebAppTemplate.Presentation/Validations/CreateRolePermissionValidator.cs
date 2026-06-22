using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs.Requests;

namespace WebAppTemplate.Presentation.Validations
{
    public class CreateRolePermissionValidator : AbstractValidator<CreateRolePermissionRequest>
    {
        public CreateRolePermissionValidator()
        {
            RuleFor(x => x.RoleId)
           .NotEmpty()
           .WithMessage("Role is required.");

            RuleFor(x => x.PermissionId)
                .NotNull()
                .WithMessage("Permissions are required.")
                .NotEmpty()
                .WithMessage("At least one permission is required.");

            RuleForEach(x => x.PermissionId)
                .NotEmpty()
                .WithMessage("Permission Id cannot be empty.");
            RuleFor(x => x.PermissionId)
                            .Must(x => x.Distinct().Count() == x.Count)
                            .WithMessage("Duplicate permissions are not allowed.");
        }
       
    }
}
