using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _33_API_CRUD.Controllers
{
    [Route("movie")]
    public class MovieController : BaseController
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _service.GetMovies();
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Get(int id)
        {
            var model = _service.GetMovie(id);
            if (model == null)
                return NotFound();
            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
            //var model = service.GetMovie(id);
            //return OkOrNotFound(model);
        }

        [HttpPost]
        public IActionResult Create([FromBody]MovieInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return Unprocessable(ModelState);
            var model = ToDomainModel(inputModel);
            _service.AddMovie(model);
            var outputModel = ToOutputModel(model);
            return CreatedAtRoute("GetMovie", new { id = outputModel.Id }, outputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]MovieInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();
            if (!_service.MovieExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return new UnprocessableObjectResult(ModelState);
            var model = ToDomainModel(inputModel);
            _service.UpdateMovie(model);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, [FromBody]JsonPatchDocument<MovieInputModel> patch)
        {
            if (patch == null)
                return BadRequest();
            var model = _service.GetMovie(id);
            if (model == null)
                return NotFound();
            var inputModel = ToInputModel(model);
            patch.ApplyTo(inputModel);
            TryValidateModel(inputModel);
            if (!ModelState.IsValid)
                return new UnprocessableObjectResult(ModelState);
            model = ToDomainModel(inputModel);
            _service.UpdateMovie(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.MovieExists(id))
                return NotFound();
            _service.DeleteMovie(id);
            return NoContent();
        }

        #region " Mappings "

        private MovieOutputModel ToOutputModel(Movie model)
        {
            return new MovieOutputModel
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary,
                LastReadAt = DateTime.Now
            };
        }

        private List<MovieOutputModel> ToOutputModel(List<Movie> model)
        {
            return model.Select(item => ToOutputModel(item))
                        .ToList();
        }

        private Movie ToDomainModel(MovieInputModel inputModel)
        {
            return new Movie
            {
                Id = inputModel.Id,
                Title = inputModel.Title,
                ReleaseYear = inputModel.ReleaseYear,
                Summary = inputModel.Summary
            };
        }

        private MovieInputModel ToInputModel(Movie model)
        {
            return new MovieInputModel
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary
            };
        }

        #endregion
    }

    public class BaseController : Controller
    {
        [NonAction]
        public UnprocessableObjectResult Unprocessable(ModelStateDictionary modelState)
        {
            return new UnprocessableObjectResult(modelState);
        }

        [NonAction]
        public ObjectResult Unprocessable(object value)
        {
            return new UnprocessableObjectResult(value);
        }

        [NonAction]
        public IActionResult OkOrNotFound(object model)
        {
            return (model == null) ? NotFound() : (IActionResult)Ok(model);
        }
    }

    public class UnprocessableObjectResult : ObjectResult
    {
        public UnprocessableObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableObjectResult(ModelStateDictionary modelState) : this(new SerializableError(modelState))
        {

        }
    }
}
