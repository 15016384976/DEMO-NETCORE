using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace _03_MIDDLEWARE_PASSING_PARAMETERS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // serviceCollection.AddMessageFormatter(new MessageOptions { Format = "upper" });
            serviceCollection.AddMessageFormatter(messageOptions =>
            {
                messageOptions.Format = "upper";
            });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            // applicationBuilder.UseGreetingMiddleware(new GreetingOptions { Content = "Greeting via Type" });
            applicationBuilder.UseGreetingMiddleware(greetingOptions =>
            {
                greetingOptions.Content = "Greeting via Func";
            });
        }
    }

    public class GreetingOptions
    {
        public string Content { get; set; }
    }

    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseGreetingMiddleware(this IApplicationBuilder applicationBuilder, GreetingOptions greetingOptions)
        {
            return applicationBuilder.UseMiddleware<GreetingMiddleware>(greetingOptions);
        }

        public static IApplicationBuilder UseGreetingMiddleware(this IApplicationBuilder applicationBuilder, Action<GreetingOptions> configureGreetingOptions)
        {
            var greetingOptions = new GreetingOptions();
            configureGreetingOptions(greetingOptions);
            return applicationBuilder.UseMiddleware<GreetingMiddleware>(greetingOptions);
        }
    }

    public class GreetingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GreetingOptions _greetingOptions;

        public GreetingMiddleware(RequestDelegate next, GreetingOptions greetingOptions)
        {
            _next = next;
            _greetingOptions = greetingOptions;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync(_greetingOptions.Content);
        }
    }

    public class MessageOptions
    {
        public string Format { get; set; }
    }

    public interface IMessageService
    {
        string FormatMessage(string message);
    }

    public class MessageService : IMessageService
    {
        private readonly MessageOptions _messageOptions;

        public MessageService(MessageOptions messageOptions)
        {
            _messageOptions = messageOptions;
        }

        public string FormatMessage(string message)
        {
            return _messageOptions.Format == "upper" ? message.ToUpper() : message.ToLower();
        }
    }

    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddMessageFormatter(this IServiceCollection serviceCollection, MessageOptions messageOptions)
        {
            return serviceCollection.AddScoped<IMessageService>(serviceProvider =>
            {
                return new MessageService(messageOptions);
            });
        }

        public static IServiceCollection AddMessageFormatter(this IServiceCollection serviceCollection, Action<MessageOptions> configureMessageOptions)
        {
            var messageOptions = new MessageOptions();
            configureMessageOptions(messageOptions);
            return serviceCollection.AddScoped<IMessageService>(serviceProvider =>
            {
                return new MessageService(messageOptions);
            });
        }
    }
}
