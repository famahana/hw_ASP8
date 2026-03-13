using Books.Application.DTOs.BookDTOS;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(IBookService _bookService, IQueueService _queue) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute]int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);    
        }
        //[Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookCreateDto bookDto)
        {
            await _queue.PublishAsync("Books", bookDto);
            int? id = await _bookService.CreateBookAsync(bookDto);
            if(id != null)
            {
                return CreatedAtAction(nameof(GetBookById), new {id},id);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
