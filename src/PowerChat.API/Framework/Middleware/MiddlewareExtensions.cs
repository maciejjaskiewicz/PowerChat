using Microsoft.AspNetCore.Builder;

namespace PowerChat.API.Framework.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionsHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}
