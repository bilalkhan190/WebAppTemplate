using FluentValidation;
using WebAppTemplate.Application.DTOs.Requests;

namespace WebAppTemplate.Presentation.Validations;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Refresh token is required.")
            .MaximumLength(500).WithMessage("Refresh token exceeds maximum length.");
    }
}
