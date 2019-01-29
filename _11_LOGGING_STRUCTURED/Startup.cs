using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace _11_LOGGING_STRUCTURED
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // setup dependency injection in service container
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseHelloLoggingMiddleware();
        }
    }

    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseHelloLoggingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<HelloLoggingMiddleware>();
        }
    }

    public class HelloLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HelloLoggingMiddleware> _logger;

        public HelloLoggingMiddleware(RequestDelegate next, ILogger<HelloLoggingMiddleware> logger)
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
}

// Install-Package Serilog.AspNetCore
// Install-Package Serilog.Sinks.Literate
// Install-Package Serilog.Sinks.Seq

// Serilog: https://serilog.net/
// Serilog Sinks: https://github.com/serilog/serilog/wiki/Provided-Sinks
// Seq: https://getseq.net/