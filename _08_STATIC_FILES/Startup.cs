using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace _08_STATIC_FILES
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // setup dependency injection in service container
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseStaticFiles(); // for files in wwwroot folder
            applicationBuilder.UseStaticFiles(new StaticFileOptions() // for files in content folder
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "content")),
                RequestPath = new PathString("/outside-content")
            });
        }
    }
}

// localhost:5000/hello.html
// localhost:5000/outside-content/hello.html
