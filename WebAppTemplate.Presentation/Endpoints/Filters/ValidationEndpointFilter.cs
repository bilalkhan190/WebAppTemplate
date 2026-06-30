using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebAppTemplate.Application.DTOs;

namespace WebAppTemplate.Presentation.Endpoints.Filters;

public sealed class ValidationEndpointFilter<TRequest> : IEndpointFilter
{
  public async ValueTask<object?> InvokeAsync(
      EndpointFilterInvocationContext context,
      EndpointFilterDelegate next)
  {
    var request = context.Arguments.OfType<TRequest>().FirstOrDefault();
    if (request is null)
      return await next(context);

    var validator = context.HttpContext.RequestServices.GetService<IValidator<TRequest>>();
    if (validator is null)
      return await next(context);

    var validationResult = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
    if (validationResult.IsValid)
      return await next(context);

    var message = string.Join(", ",
        validationResult.Errors.Select(error =>
            string.IsNullOrWhiteSpace(error.ErrorMessage)
                ? "Validation failed."
                : error.ErrorMessage));

    return Results.Json(
        new ApiResponse<object?>(false, message, null),
        statusCode: StatusCodes.Status400BadRequest);
  }
}
