using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.IO;

namespace _30_MVC_UPLOAD_DOWNLOAD_FILES
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
    }

    public class FileDetails
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class FilesViewModel
    {
        public List<FileDetails> Files { get; set; } = new List<FileDetails>();
    }
}
