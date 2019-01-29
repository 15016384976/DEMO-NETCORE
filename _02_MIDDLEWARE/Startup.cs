using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace _02_MIDDLEWARE
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // setup dependency injection in service container
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseHelloWorld();
            applicationBuilder.UseHelloWorldInClass();
            applicationBuilder.RunHelloWorld();
        }
    }

    public static class IApplicationBuilderExtension
    {
        public static void RunHelloWorld(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Run(async (httpContext) =>
            {
                await httpContext.Response.WriteAsync("Hello World (via Run)\n");
            });
        }

        public static IApplicationBuilder UseHelloWorld(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.Use(async (httpContext, next) =>
            {
                await httpContext.Response.WriteAsync("Hello World (via Use)\n");
                await next();
            });
        }

        public static IApplicationBuilder UseHelloWorldInClass(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<HelloWorldMiddleware>();
        }
    }

    public class HelloWorldMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloWorldMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync("Hello World (via UseInClass)\n");
            await _next(httpContext);
        }
    }
}
