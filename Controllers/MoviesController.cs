using LibraryAPI.Models;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _movieRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovies(int id)
        {
            return await _movieRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovies([FromBody] Movie movie)
        {
            var newMovie = _movieRepository.Create(movie);
            return CreatedAtAction(nameof(GetMovies), new { id = newMovie.Id }, newMovie);
        }

        [HttpPut]
        public async Task<ActionResult> PutMovies(int id, [FromBody] Movie movie)
        {
            if(id != movie.Id)
            {
                BadRequest();
            }

            await _movieRepository.Update(movie);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movieToDelete = await _movieRepository.Get(id);
            if(movieToDelete == null)
            {
                return NotFound();
            }
            await _movieRepository.Delete(movieToDelete.Id);

            return NoContent();
        }
    }
}
