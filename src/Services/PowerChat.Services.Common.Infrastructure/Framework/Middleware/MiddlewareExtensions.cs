using Microsoft.AspNetCore.Builder;

namespace PowerChat.Services.Common.Infrastructure.Framework.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionsHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}
