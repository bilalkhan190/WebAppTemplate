using Microsoft.AspNetCore.Http;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.DTOs;
using WebAppTemplate.Domain.Enums;

namespace WebAppTemplate.Presentation.Endpoints.Extensions;

public static class EndpointResultExtensions
{
  public static IResult ToHttpResult<T>(this ServiceResult<T> result)
  {
    return result switch
    {
      ServiceResult<T>.Success success =>
          Results.Json(
              new ApiResponse<T>(true, "Operation Successful", success.Entity),
              statusCode: StatusCodes.Status200OK),

      ServiceResult<T>.Failure failure when failure.ErrorType == ErrorType.Validation =>
          Results.Json(
              new ApiResponse<object?>(false, string.Join(", ", failure.Errors), null),
              statusCode: StatusCodes.Status400BadRequest),

      ServiceResult<T>.Failure failure when failure.ErrorType == ErrorType.NotFound =>
          Results.Json(
              new ApiResponse<object?>(false, string.Join(", ", failure.Errors), null),
              statusCode: StatusCodes.Status404NotFound),

      ServiceResult<T>.Failure failure when failure.ErrorType == ErrorType.Conflict =>
          Results.Json(
              new ApiResponse<object?>(false, string.Join(", ", failure.Errors), null),
              statusCode: StatusCodes.Status409Conflict),

      ServiceResult<T>.Failure failure when failure.ErrorType == ErrorType.Unexpected =>
          Results.Json(
              new ApiResponse<object?>(false, string.Join(", ", failure.Errors), null),
              statusCode: StatusCodes.Status500InternalServerError),

      _ => Results.Json(
          new ApiResponse<object?>(false, "An unexpected error occurred", null),
          statusCode: StatusCodes.Status500InternalServerError)
    };
  }

  public static async Task<IResult> ToHttpResult<T>(this Task<ServiceResult<T>> operation)
      => (await operation).ToHttpResult();
}
