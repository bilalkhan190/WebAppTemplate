using FluentValidation;
using WebAppTemplate.Application.DTOs.Requests;

namespace WebAppTemplate.Presentation.Validations;

public class CreateRoleAssignmentRequestValidator : AbstractValidator<CreateRoleAssignmentRequest>
{
    public CreateRoleAssignmentRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id is required.");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("Role id is required.");
    }
}
