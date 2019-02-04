using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace _33_API_CRUD
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMovieService, MovieService>();
            serviceCollection.AddSingleton<IReviewService, ReviewService>();
            serviceCollection.AddMvc();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            applicationBuilder.UseMvcWithDefaultRoute();
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
        private readonly List<Movie> _movies;

        public MovieService()
        {
            _movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Never Say Never Again", ReleaseYear = 1983, Summary = "A SPECTRE agent has stolen two American nuclear warheads, and James Bond must find their targets before they are detonated." },
                new Movie { Id = 2, Title = "Diamonds Are Forever ", ReleaseYear = 1971, Summary = "A diamond smuggling investigation leads James Bond to Las Vegas, where he uncovers an evil plot involving a rich business tycoon." },
                new Movie { Id = 3, Title = "You Only Live Twice ", ReleaseYear = 1967, Summary = "Agent 007 and the Japanese secret service ninja force must find and stop the true culprit of a series of spacejackings before nuclear war is provoked." }
            };
        }

        public List<Movie> GetMovies()
        {
            return _movies.ToList();
        }

        public Movie GetMovie(int id)
        {
            return _movies.Where(m => m.Id == id).FirstOrDefault();
        }

        public void AddMovie(Movie item)
        {
            _movies.Add(item);
        }

        public void UpdateMovie(Movie item)
        {
            var model = _movies.Where(m => m.Id == item.Id).FirstOrDefault();

            model.Title = item.Title;
            model.ReleaseYear = item.ReleaseYear;
            model.Summary = item.Summary;
        }

        public void DeleteMovie(int id)
        {
            var model = _movies.Where(m => m.Id == id).FirstOrDefault();

            _movies.Remove(model);
        }

        public bool MovieExists(int id)
        {
            return _movies.Any(m => m.Id == id);
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

    public class Review
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string Comments { get; set; }
        public int MovieId { get; set; }
    }

    public interface IReviewService
    {
        List<Review> GetReviews(int movieId);
        Review GetReview(int movieId, int id);
        void AddReview(Review item);
        void UpdateReview(Review item);
        void DeleteReview(int id);
        bool ReviewExists(int movieId, int id);
    }

    public class ReviewService : IReviewService
    {
        private readonly List<Review> _reviews;

        public ReviewService()
        {
            _reviews = new List<Review>
            {
                new Review { Id = 1, MovieId = 1, Reviewer = "Tahir", Comments = "Excellent" },
                new Review { Id = 2, MovieId = 1, Reviewer = "Tony", Comments = "Good" },
                new Review { Id = 3, MovieId = 2, Reviewer = "Tahir", Comments = "Very Good" },
                new Review { Id = 4, MovieId = 3, Reviewer = "Tahir", Comments = "Average" },
                new Review { Id = 5, MovieId = 3, Reviewer = "Robert", Comments = "Interesting" },
            };
        }

        public List<Review> GetReviews(int movieId)
        {
            return _reviews.Where(r => r.MovieId == movieId).ToList();
        }

        public Review GetReview(int movieId, int id)
        {
            return _reviews.Where(r => r.MovieId == movieId && r.Id == id).FirstOrDefault();
        }

        public void AddReview(Review item)
        {
            _reviews.Add(item);
        }

        public void UpdateReview(Review item)
        {
            var model = _reviews.Where(r => r.Id == item.Id).FirstOrDefault();

            model.Reviewer = item.Reviewer;
            model.Comments = item.Comments;
        }

        public void DeleteReview(int id)
        {
            var model = _reviews.Where(r => r.Id == id).FirstOrDefault();

            _reviews.Remove(model);
        }

        public bool ReviewExists(int movieId, int id)
        {
            return _reviews.Any(r => r.MovieId == movieId && r.Id == id);
        }
    }

    public class ReviewInputModel
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string Comments { get; set; }
        public int MovieId { get; set; }
    }

    public class ReviewOutputModel
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string Comments { get; set; }
        public int MovieId { get; set; }
        public DateTime LastReadAt { get; set; }
    }
}
