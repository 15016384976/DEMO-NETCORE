using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace _36_API_SORTING.Controllers
{
    [Route("movie")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetMovies")]
        public IActionResult Get(SortingParams sortingParams)
        {
            var model = _service.GetMovies(sortingParams);

            var outputModel = new MovieOutputModel
            {
                Items = model.Select(m => ToMovieInfo(m)).ToList(),
            };
            return Ok(outputModel);
        }

        #region " Mappings "
        private MovieInfo ToMovieInfo(Movie model)
        {
            return new MovieInfo
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary,
                LeadActor = model.LeadActor,
                LastReadAt = DateTime.Now
            };
        }
        #endregion
    }
}
