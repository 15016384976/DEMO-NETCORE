using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace _04_DEPENDENCY_INJECTION
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // serviceCollection.AddScoped<IGreetingService, GreetingService>();

            serviceCollection.AddScoped<IGreetingService>(implementationFactory =>
            {
                return new GreetingServiceFlexible("Prefix");
            });

            // serviceCollection.AddSingleton<IGreetingService>(new GreetingServiceFlexible("Prefix"));
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseGreetingMiddleware();
        }
    }

    public interface IGreetingService
    {
        string Greeting(string message);
    }

    public class GreetingService : IGreetingService
    {
        public string Greeting(string message) => message;
    }

    public class GreetingServiceFlexible : IGreetingService
    {
        public readonly string _prefix;

        public GreetingServiceFlexible(string prefix)
        {
            _prefix = prefix;
        }

        public string Greeting(string message) => _prefix + " " + message;
    }

    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseGreetingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<GreetingMiddleware>();
        }
    }

    public class GreetingMiddleware
    {
        private readonly RequestDelegate _next;

        public GreetingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var greetingService = httpContext.RequestServices.GetService<IGreetingService>();
            var message = greetingService.Greeting("Hello World (via DI)\n");
            await httpContext.Response.WriteAsync(message);
        }
    }
}
