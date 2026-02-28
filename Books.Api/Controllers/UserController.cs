
using Books.Application.DTOs.UserDto;
using Books.Application.Interfaces.Services;
using Books.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController(IUserService _userService):ControllerBase
    {      
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUserAsync();
            return Ok(users);
        }
        [HttpGet("{email}")]
        
        public async Task<IActionResult> GetUserByEmail([FromRoute]string email)
        {
            var user = await _userService.GetByEmailUserAsync(email);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        //{
        //    return Ok();
        //    //var token = await _userService.LoginAsync(dto);
        //    //return Ok(new { accessToken = token });
        //}
        //[HttpPost]
        //public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        //{
        //    return Ok();
        //    //var email = await _userService.CreateUserAsync(dto);

        //    //if (email != null)
        //    //{
        //    //    return CreatedAtAction(nameof(GetUserByEmail), new { email }, email);
        //    //}
        //    //else
        //    //{
        //    //    return BadRequest();
        //    //}
        //}


    }
}
