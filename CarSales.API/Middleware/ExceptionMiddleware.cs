using CarSales.Application.Comman;
using CarSales.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace CarSales.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Proceed to next
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Caught in middleware: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = exception switch
            {
                BadRequestException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                ForbiddenAccessException => HttpStatusCode.Forbidden,
                TimeoutException => HttpStatusCode.GatewayTimeout,
                CustomValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            response.StatusCode = (int)statusCode;

            var errorResponse = exception switch
            {
                CustomValidationException validationEx => new ApiResponse<object>
                {
                    Succeeded = false,
                    Message = validationEx.Message,
                    Errors = validationEx.Errors,
                    StatusCode = statusCode
                },
                BadRequestException badReq => new ApiResponse<object>
                {
                    Succeeded = false,
                    Message = badReq.Message,
                    StatusCode = statusCode
                },
                UnauthorizedAccessException => new ApiResponse<object>
                {
                    Succeeded = false,
                    Message = "Unauthorized access",
                    StatusCode = statusCode
                },
                ForbiddenAccessException => new ApiResponse<object>
                {
                    Succeeded = false,
                    Message = "Access forbidden",
                    StatusCode = statusCode
                },
                TimeoutException => new ApiResponse<object>
                {
                    Succeeded = false,
                    Message = "Request timed out",
                    StatusCode = statusCode
                },
                NotImplementedException => new ApiResponse<object>
                {
                    Succeeded = false,
                    Message = "Feature not implemented",
                    StatusCode = statusCode
                },
                NotFoundException notFound => new ApiResponse<object>
                {
                    Succeeded = false,
                    Message = notFound.Message,
                    StatusCode = statusCode
                },
                _ => new ApiResponse<object>
                {
                    Succeeded = false,
                    Message = "Internal Server Error",
                    StatusCode = statusCode
                }
            };

            var result = JsonSerializer.Serialize(errorResponse);
            return response.WriteAsync(result);
        }

        //private Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    var response = context.Response;
        //    response.ContentType = "application/json";

        //    var statusCode = exception switch
        //    {
        //        BadRequestException => HttpStatusCode.BadRequest,
        //        NotFoundException => HttpStatusCode.NotFound,
        //        CustomValidationException => HttpStatusCode.BadRequest,
        //        _ => HttpStatusCode.InternalServerError
        //    };

        //    response.StatusCode = (int)statusCode;

        //    var errorResponse = exception switch
        //    {
        //        CustomValidationException validationEx => new ApiResponse<object>
        //        {
        //            Succeeded = false,
        //            Message = validationEx.Message,
        //            Errors = validationEx.Errors,
        //            StatusCode = statusCode
        //        },
        //        BadRequestException badReq => new ApiResponse<object>
        //        {
        //            Succeeded = false,
        //            Message = badReq.Message,
        //            StatusCode = statusCode
        //        },
        //        NotFoundException notFound => new ApiResponse<object>
        //        {
        //            Succeeded = false,
        //            Message = notFound.Message,
        //            StatusCode = statusCode
        //        },
        //        _ => new ApiResponse<object>
        //        {
        //            Succeeded = false,
        //            Message = "Internal Server Error",
        //            StatusCode = statusCode
        //        }
        //    };

        //    var result = JsonSerializer.Serialize(errorResponse);
        //    return response.WriteAsync(result);
        //}

        //private Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    var response = context.Response;
        //    response.ContentType = "application/json";

        //    var statusCode = exception switch
        //    {
        //        BadRequestException => HttpStatusCode.BadRequest,
        //        NotFoundException => HttpStatusCode.NotFound,
        //        CustomValidationException => HttpStatusCode.BadRequest,
        //        _ => HttpStatusCode.InternalServerError
        //    };

        //    response.StatusCode = (int)statusCode;

        //    var result = exception switch
        //    {
        //        CustomValidationException validationEx => JsonSerializer.Serialize(new
        //        {
        //            message = validationEx.Message,
        //            errors = validationEx.Errors
        //        }),
        //        _ => JsonSerializer.Serialize(new { message = exception.Message })
        //    };

        //    return response.WriteAsync(result);
        //}
    }

}
