using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace _12_SESSION_STATE
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDistributedMemoryCache();
            serviceCollection.AddSession(configure =>
            {
                configure.Cookie.HttpOnly = true;
                configure.Cookie.Name = ".Session";
                configure.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                configure.IdleTimeout = TimeSpan.FromMinutes(10);
            });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseSession();

            applicationBuilder.Use(async (httpContext, next) =>
            {
                // httpContext.Session.SetString("GreetingMessage", "Greeting Message");
                httpContext.Session.SetObject("CurrentUser", new User { FirstName = "F", LastName = "L" });
                await next();
            });

            applicationBuilder.Run(async httpContext =>
            {
                // var message = httpContext.Session.GetString("GreetingMessage");
                // await httpContext.Response.WriteAsync(message);
                var currentUser = httpContext.Session.GetObject<User>("CurrentUser");
                await httpContext.Response.WriteAsync($"{currentUser.FirstName} {currentUser.LastName}");
            });
        }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public static class ISessionExtension
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    // IHttpContextAccessor via constructor -> HttpContext
}
