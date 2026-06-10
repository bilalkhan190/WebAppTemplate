using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Application.DTOs;

namespace WebAppTemplate.Application.Extensions
{
    public static class ApiExtension
    {
        public static IActionResult Success<T>(T data = default)
        {
            var response = new ApiResponse<T>(true, "Opeation Successfull", data);
            return new OkObjectResult(response);
        }

        public static IActionResult Success<T>(T data = default, string message = "")
        {
            var response = new ApiResponse<T>(true, message, data);
            return new OkObjectResult(response);
        }

        public static IActionResult Failure(string errorMessage = "", int statusCode = 400)
        {
            var response = new ApiResponse<object>(false, errorMessage, null);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        public static IActionResult Failure(string errorMessage = "", object data = default, int statusCode = 400)
        {
            var response = new ApiResponse<object>(false, errorMessage, data);
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        public static IActionResult Unauthorized(string errorMessage = "Unauthorized")
        {
            var response = new ApiResponse<object>(false, errorMessage, null);
            return new ObjectResult(response) { StatusCode = 401 };
        }

        public static IActionResult InternalServerError(string errorMessage = "An unexpected error occurred", object data = null)
        {
            var response = new ApiResponse<object>(false, errorMessage, data);
            return new ObjectResult(response) { StatusCode = 500 };
        }

        public static IActionResult NotFound(string errorMessage = "Resource not found")
        {
            var response = new ApiResponse<object>(false, errorMessage, null);
            return new NotFoundObjectResult(response) { StatusCode = 404 };
        }
        
    }
}
