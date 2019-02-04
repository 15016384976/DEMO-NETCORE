using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace _32_MVC_RAZOR_PAGES
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMovieService, MovieService>();
            serviceCollection.AddMvc();
            //services.AddMvc()
            //        .AddRazorPagesOptions(options =>
            //        {
            //            options.RootDirectory = "/MyPages";
            //        });
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvc();
        }
    }

    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }
    }

    public interface IMovieService
    {
        List<Movie> GetMovies();
        Movie GetMovie(int id);
        void AddMovie(Movie item);
        void UpdateMovie(Movie item);
        void DeleteMovie(int id);
        bool MovieExists(int id);
    }

    public class MovieService : IMovieService
    {
        private readonly List<Movie> movies;

        public MovieService()
        {
            this.movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Never Say Never Again", ReleaseYear = 1983, Summary = "A SPECTRE agent has stolen two American nuclear warheads, and James Bond must find their targets before they are detonated." },
                new Movie { Id = 2, Title = "Diamonds Are Forever ", ReleaseYear = 1971, Summary = "A diamond smuggling investigation leads James Bond to Las Vegas, where he uncovers an evil plot involving a rich business tycoon." },
                new Movie { Id = 3, Title = "You Only Live Twice ", ReleaseYear = 1967, Summary = "Agent 007 and the Japanese secret service ninja force must find and stop the true culprit of a series of spacejackings before nuclear war is provoked." }
            };
        }

        public List<Movie> GetMovies()
        {
            return this.movies.ToList();
        }

        public Movie GetMovie(int id)
        {
            return this.movies.Where(m => m.Id == id).FirstOrDefault();
        }

        public void AddMovie(Movie item)
        {
            this.movies.Add(item);
        }

        public void UpdateMovie(Movie item)
        {
            var model = this.movies.Where(m => m.Id == item.Id).FirstOrDefault();

            model.Title = item.Title;
            model.ReleaseYear = item.ReleaseYear;
            model.Summary = item.Summary;
        }

        public void DeleteMovie(int id)
        {
            var model = this.movies.Where(m => m.Id == id).FirstOrDefault();

            this.movies.Remove(model);
        }

        public bool MovieExists(int id)
        {
            return this.movies.Any(m => m.Id == id);
        }
    }

    public class MovieInputModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }
    }

    public class MovieOutputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }
        public DateTime LastReadAt { get; set; }
    }
}
