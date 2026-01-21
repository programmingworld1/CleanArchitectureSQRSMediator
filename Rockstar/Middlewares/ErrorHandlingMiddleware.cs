using Application.Errors;
using FluentValidation;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rockstar.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "An error occurred while processing your request";
            var code = "InternalServerError";
            object? errors = null;

            if (exception is HttpException httpEx)
            {
                statusCode = httpEx.StatusCode;
                message = httpEx.Message;
                code = httpEx.ErrorCode;
            }
            else if (exception is ValidationException validationEx)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = validationEx.Message;
                code = "ValidationFailed";

                errors = validationEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                code,
                error = message,
                errors
            });

            return context.Response.WriteAsync(result);
        }
    }
}
