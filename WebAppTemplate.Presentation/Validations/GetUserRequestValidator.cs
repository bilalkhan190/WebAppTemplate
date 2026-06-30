using FluentValidation;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Requests.GET;

namespace WebAppTemplate.Presentation.Validations;

public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
{
    public GetUserRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}
