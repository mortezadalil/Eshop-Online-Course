using Microsoft.AspNetCore.Http;

namespace Eshop.Middlewares
{
    public class DemoMiddleware
    {
        private readonly RequestDelegate _next;
        public DemoMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
          
            if (context.Request.Path.StartsWithSegments("/hello"))
            {
                await context.Response.WriteAsync("Hello, World!");
            }
            else
            {
                Console.WriteLine("Demo Middleware logic from the separate class.");
                await _next.Invoke(context);
            }

    
        }
    }

    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseDemoMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<DemoMiddleware>();
    }
}
