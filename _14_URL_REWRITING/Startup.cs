using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.Linq;

namespace _14_URL_REWRITING
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // setup dependency injection in service container
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            // var rewrite = new RewriteOptions().AddRedirect("films", "movies").AddRewrite("actors", "stars", true);
            var rewrite = new RewriteOptions().Add(new MoviesRedirectRule(matchPaths: new[] { "/films", "/features", "/albums" }, newPath: "/movies"));
            applicationBuilder.UseRewriter(rewrite);
            applicationBuilder.Run(async httpContext =>
            {
                var path = httpContext.Request.Path;
                var query = httpContext.Request.QueryString;
                await httpContext.Response.WriteAsync($"New URL: {path}{query}");
            });
        }
    }

    public class MoviesRedirectRule : IRule
    {
        private readonly string[] _matchPaths;
        private readonly PathString _newPath;

        public MoviesRedirectRule(string[] matchPaths, string newPath)
        {
            _matchPaths = matchPaths;
            _newPath = new PathString(newPath);
        }

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Path.StartsWithSegments(new PathString(_newPath)))
            {
                return;
            }

            if (_matchPaths.Contains(request.Path.Value))
            {
                var newLocation = $"{_newPath}{request.QueryString}";

                var response = context.HttpContext.Response;
                response.StatusCode = StatusCodes.Status302Found;
                context.Result = RuleResult.EndResponse;
                response.Headers[HeaderNames.Location] = newLocation;
            }
        }
    }
}
