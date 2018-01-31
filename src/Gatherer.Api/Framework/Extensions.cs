using Microsoft.AspNetCore.Builder;

namespace Gatherer.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            =>builder.UseMiddleware(typeof(ErrorHandlerMiddleware));
    }
}