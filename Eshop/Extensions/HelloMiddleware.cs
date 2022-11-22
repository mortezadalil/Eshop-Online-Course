using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Eshop.Extensions
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HelloMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/hello"))
            {
                await httpContext.Response.WriteAsync("Hello, World!");
            }
            else
            {
                await _next(httpContext);

            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HelloMiddlewareExtensions
    {
        public static IApplicationBuilder UseHelloMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleware>();
        }
    }
}
