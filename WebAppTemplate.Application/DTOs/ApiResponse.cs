using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppTemplate.Application.DTOs
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }

        public T Data { get; set; }

        public bool Success {  get; set; }

        public ApiResponse(bool success, string message, T data)
        {
                Message = message;
                Data = data;
                Success = success;
        }
    }
}
