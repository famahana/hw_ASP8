using Books.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController(IAuthorService _authorService):ControllerBase
    {

    }
}
