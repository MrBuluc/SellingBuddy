using FluentValidation;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace WebApiGateway.Application.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = GetStatusCode(ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            if (ex.GetType() == typeof(ValidationException))
            {
                return context.Response.WriteAsync(new ExceptionModel
                {
                    Errors = ((ValidationException)ex).Errors.Select(e => e.ErrorMessage),
                    StatusCode = StatusCodes.Status400BadRequest
                }.ToString());
            }

            return context.Response.WriteAsync(new ExceptionModel
            {
                Errors = new List<string>()
                {
                    $"Error Message: {ex.Message}",
                    $"Message Description: {ex.InnerException?.ToString()}"
                },
                StatusCode = statusCode
            }.ToString());
        }

        private static int GetStatusCode(Exception ex) => ex switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status204NoContent,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
