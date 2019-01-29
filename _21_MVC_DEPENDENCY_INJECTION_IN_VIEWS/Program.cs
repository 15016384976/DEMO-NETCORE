using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace _21_MVC_DEPENDENCY_INJECTION_IN_VIEWS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
