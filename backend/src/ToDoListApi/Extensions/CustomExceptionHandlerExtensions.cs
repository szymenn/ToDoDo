using Microsoft.AspNetCore.Builder;

namespace ToDoListApi.Extensions
{
    public static class CustomExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}