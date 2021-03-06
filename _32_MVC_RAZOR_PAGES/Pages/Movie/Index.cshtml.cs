using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _32_MVC_RAZOR_PAGES.Views.Movie
{
    public class IndexModel : PageModel
    {
        private readonly IMovieService service;

        public IndexModel(IMovieService service)
        {
            this.service = service;
        }

        public List<MovieOutputModel> Movies { get; set; }

        public void OnGet()
        {
            this.Movies = this.service.GetMovies()
                                      .Select(item => new MovieOutputModel
                                      {
                                          Id = item.Id,
                                          Title = item.Title,
                                          ReleaseYear = item.ReleaseYear,
                                          Summary = item.Summary,
                                          LastReadAt = DateTime.Now
                                      })
                                      .ToList();
        }

        public IActionResult OnGetDelete1(int id)
        {
            if (!service.MovieExists(id))
                return RedirectToPage("./Index");

            service.DeleteMovie(id);

            return RedirectToPage("./Index");
        }

        public IActionResult OnPostDelete2(int id)
        {
            if (!service.MovieExists(id))
                return RedirectToPage("./Index");

            service.DeleteMovie(id);

            return RedirectToPage("./Index");
        }
    }
}