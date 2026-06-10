using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.Common.Results;
using WebAppTemplate.Application.Extensions;
using WebAppTemplate.Domain.Enums;

namespace WebAppTemplate.Presentation.Extensions
{
    public static class ServiceResultExtension
    {
        public static IActionResult ToActionResult<T>(this ServiceResult<T> result)
        {
            return result switch
            {
                ServiceResult<T>.Success success
                    => ApiExtension.Success(success.Entity),

                ServiceResult<T>.Failure failure
                    when failure.ErrorType == ErrorType.Validation
                        => ApiExtension.Failure(
                            string.Join(", ", failure.Errors),
                            StatusCodes.Status400BadRequest),

                ServiceResult<T>.Failure failure
                    when failure.ErrorType == ErrorType.NotFound
                        => ApiExtension.NotFound(
                            string.Join(", ", failure.Errors)),

                ServiceResult<T>.Failure failure
                    when failure.ErrorType == ErrorType.Conflict
                        => ApiExtension.Failure(
                            string.Join(", ", failure.Errors),
                            StatusCodes.Status409Conflict),

                ServiceResult<T>.Failure failure
                    when failure.ErrorType == ErrorType.Unexpected
                        => ApiExtension.InternalServerError(
                            string.Join(", ", failure.Errors)),

                _ => ApiExtension.InternalServerError()
            };
        }
    }
}
