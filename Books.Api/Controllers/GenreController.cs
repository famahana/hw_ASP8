using Books.Application.DTOs.GenreDto;
using Books.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController(IGenreService _genreService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.getAllGenreAsync();
            return Ok(genres);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById([FromRoute] int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            return Ok(genre);

        }
        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreCreateDto genreDto)
        {
            var result = await _genreService.AddGenreAsync(genreDto);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre([FromRoute] int id, [FromBody] GenreCreateDto genreDto)
        {
            var result = await _genreService.UpdateGenreAsync(id, genreDto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id)
        {
            var result = await _genreService.DeleteGenreAsync(id);
            return Ok(result);
        }
    }
    }
