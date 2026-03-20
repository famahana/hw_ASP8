using Books.Application.DTOs.CityDto;
using Books.Application.Query.Country;
using Books.Infrastructure.Command.City;
using Books.Infrastructure.Query.City;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(IMediator _mediatr) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody]CreateCityDto dto)
        {
            return Ok(await _mediatr.Send(new CreateCityCommand(dto.Name, dto.CountryId)));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCity()
        {
            return Ok(await _mediatr.Send(new GetAllCitiesQuery()));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity([FromRoute] int id, string name)
        {
            return Ok(await _mediatr.Send(new UpdateCityCommand(id,name)));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] int id)
        {
            return Ok(await _mediatr.Send(new DeleteCityCommand(id)));
        }


    }
}
