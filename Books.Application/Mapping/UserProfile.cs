using AutoMapper;
using Books.Application.DTOs.UserDto;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Mapping
{
    //.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(
                //src => src.Password));
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, UserEntity>();

            CreateMap<UserEntity, UserReadDto>().ForMember(dest=>dest.Role, opt=> opt.MapFrom(
                src=>src.Role.ToString()));
        }
        
    }
}
