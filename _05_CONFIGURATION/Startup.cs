using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace _05_CONFIGURATION
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(Configuration);
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseHelloWorldMiddleware();
        }
    }

    public class AppSettings
    {
        public AppSetingsSection1 Section1 { get; set; }
        public AppSetingsSection2 Section2 { get; set; }
    }

    public class AppSetingsSection1
    {
        public string SettingA { get; set; }
        public string SettingB { get; set; }
    }

    public class AppSetingsSection2
    {
        public string SettingC { get; set; }
    }

    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseHelloWorldMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<HelloWorldMiddleware>();
        }
    }

    public class HelloWorldMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _settings;

        public HelloWorldMiddleware(RequestDelegate next, IOptions<AppSettings> options)
        {
            _next = next;
            _settings = options.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(_settings));
        }
    }
}
