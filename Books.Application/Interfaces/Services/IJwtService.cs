using Books.Application.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(UserLoginDto userLoginDto, string role);
    }
}
