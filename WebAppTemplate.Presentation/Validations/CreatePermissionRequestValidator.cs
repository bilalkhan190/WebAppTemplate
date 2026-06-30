using FluentValidation;
using WebAppTemplate.Application.DTOs.Requests;

namespace WebAppTemplate.Presentation.Validations;

public class CreatePermissionRequestValidator : AbstractValidator<CreatePermissionRequest>
{
    public CreatePermissionRequestValidator()
    {
        RuleFor(x => x.permissionName)
            .NotEmpty().WithMessage("Permission name is required.")
            .MaximumLength(100);

        RuleFor(x => x.permissionCode)
            .NotEmpty().WithMessage("Permission code is required.")
            .MaximumLength(50)
            .Matches("^[a-z0-9.]+$")
            .WithMessage("Permission code must be lowercase alphanumeric with dots (e.g. users.read).");
    }
}
