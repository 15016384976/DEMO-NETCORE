using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace _13_FILE_PROVIDERS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseHelloFileProviderMiddleware();
        }
    }

    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseHelloFileProviderMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<HelloFileProviderMiddleware>();
        }
    }

    public class HelloFileProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IFileProvider _fileProvider;

        public HelloFileProviderMiddleware(RequestDelegate next, IFileProvider fileProvider)
        {
            _next = next;
            _fileProvider = fileProvider;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var output1 = new StringBuilder("");
            var items = _fileProvider.GetDirectoryContents("");
            foreach (var item in items)
            {
                output1.AppendLine(item.Name);
            }
            await httpContext.Response.WriteAsync(output1.ToString());

            var info = _fileProvider.GetFileInfo("Program.cs");
            using (var stream = info.CreateReadStream())
            using (var reader = new StreamReader(stream))
            {
                var output2 = await reader.ReadToEndAsync();
                await httpContext.Response.WriteAsync(output2.ToString());
            }
        }
    }
}
