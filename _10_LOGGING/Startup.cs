using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace _10_LOGGING
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // setup dependency injection in service container
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseHelloLoggingMiddleware1();
            // applicationBuilder.UseHelloLoggingMiddleware2();
        }
    }

    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseHelloLoggingMiddleware1(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<HelloLoggingMiddleware1>();
        }

        public static IApplicationBuilder UseHelloLoggingMiddleware2(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<HelloLoggingMiddleware2>();
        }
    }

    public class HelloLoggingMiddleware1
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HelloLoggingMiddleware1> _logger;

        public HelloLoggingMiddleware1(RequestDelegate next, ILogger<HelloLoggingMiddleware1> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation(101, "Invoke executing");
            await httpContext.Response.WriteAsync("Hello Logging");
            _logger.LogInformation(201, "Invoke executed");
        }
    }

    public class HelloLoggingMiddleware2
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public HelloLoggingMiddleware2(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("HelloLoggingMiddleware2");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation(101, "Invoke executing");
            await httpContext.Response.WriteAsync("Hello Logging");
            _logger.LogInformation(201, "Invoke executed");
        }
    }
}
