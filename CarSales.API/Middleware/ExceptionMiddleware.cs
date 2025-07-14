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
                _logger.LogError(ex, $"Exception caught in middleware: {ex.Message}");
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
                CustomValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            response.StatusCode = (int)statusCode;

            var result = exception switch
            {
                CustomValidationException validationEx => JsonSerializer.Serialize(new
                {
                    message = validationEx.Message,
                    errors = validationEx.Errors
                }),
                _ => JsonSerializer.Serialize(new { message = exception.Message })
            };

            return response.WriteAsync(result);
        }
    }

}
