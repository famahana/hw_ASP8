using Books.Application.DTOs.AuthorDto;
using Books.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController(IAuthorService _authorService,IQueueService _queue):ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors(CancellationToken cancellation)
        {
            var authors = await _authorService.getAllAuthorAsync(cancellation);
            return Ok(authors);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById([FromRoute] int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            return Ok(author);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDto authorDto,CancellationToken cancellation)
        {

            await _queue.PublishAsync("Authors",authorDto);
            int? id = await _authorService.CreateAuthorAsync(authorDto, cancellation);
            if (id != null)
            {
                return CreatedAtAction(nameof(GetAuthorById), new { id }, id);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            var response = await _authorService.DeleteAuthorAsync(id);
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] int id, [FromBody] AuthorCreateDto authorDto)
        {
            var response = await _authorService.UpdateAuthorAsync(authorDto, id);
            return Ok(response);
        }

    }
}
