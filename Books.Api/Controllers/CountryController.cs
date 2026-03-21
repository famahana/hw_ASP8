using Books.Application.Query.Country;
using Books.Infrastructure.Command.Country;
using Books.Infrastructure.Query.Country;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController(IMediator _mediatr) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById([FromRoute]int id)
        {
            return Ok(await _mediatr.Send(new GetCountryByIdQuery(id)));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            return Ok(await _mediatr.Send(new GetAllCountryQuery()));
        }
        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody]string name)
        {
            return Ok(await _mediatr.Send(new CreateCountryCommand(name)));
        }
    }
}
