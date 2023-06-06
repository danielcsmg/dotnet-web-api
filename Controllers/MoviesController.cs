using FilmesAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    [HttpGet("/catalogo-de-filmes")]
    public IActionResult GetMovies()
    {
        var filmes = MovieResponse.GetMovies();
        if (filmes != null)
            return Ok(MovieResponse.GetMovies());

        return NotFound();
    }

    [HttpGet("/filme/{id}")]
    public IActionResult GetMoviesById([FromRoute] int id)
    {
        var movie = MovieResponse.GetMovieById(id);
        if (movie != null)
        {
            return Ok(movie);
        }
        return NotFound();
    }

    [HttpGet("/filmes/ano/{year}")]
    public IActionResult GetMoviesByYear([FromRoute] int year)
    {
        var movie = MovieResponse.GetMoviesByYear(year);
        if (movie != null)
        {
            return Ok(movie);
        }
        return NotFound();
    }

    [HttpGet("/filmes")]
    public IActionResult GetMoviesByGender([FromQuery] string genre)
    {
        var moviesByGenre = MovieResponse.GetMoviesByGenre(genre);
        if (moviesByGenre != null)
        {
            return Ok(moviesByGenre);
        }
        return BadRequest();
    }
}
