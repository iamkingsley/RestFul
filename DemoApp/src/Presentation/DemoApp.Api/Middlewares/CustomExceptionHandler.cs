using DemoApp.Contracts.Dtos;
using System.Net;
using System.Text.Json;

namespace DemoApp.Api.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var errors = new List<string>() { ex.Message };

            var result = new BaseResponse<string>()
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Errors = errors
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
