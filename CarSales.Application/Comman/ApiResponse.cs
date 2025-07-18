using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Application.Comman
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(T data, string message = null)
        {
            Succeeded = true;
            Data = data;
            Message = message;
            StatusCode = HttpStatusCode.OK;
        }

        public ApiResponse(string message, bool succeeded = false)
        {
            Succeeded = succeeded;
            Message = message;
            StatusCode = succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        }

        public static ApiResponse<T> Success(T data, string message = null, object meta = null) =>
            new ApiResponse<T>(data, message) { Meta = meta, StatusCode = HttpStatusCode.OK };

        public static ApiResponse<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, List<string> errors = null) =>
            new ApiResponse<T>(message, false) { StatusCode = statusCode, Errors = errors };
    }
}
