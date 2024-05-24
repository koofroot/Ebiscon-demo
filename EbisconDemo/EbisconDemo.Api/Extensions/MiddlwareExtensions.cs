using EbisconDemo.Api.Middlewares;

namespace EbisconDemo.Api.Extensions
{
    public static class MiddlwareExtensions
    {
        public static IApplicationBuilder UseServerErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ServerErrorMiddleware>();
        }
    }
}
