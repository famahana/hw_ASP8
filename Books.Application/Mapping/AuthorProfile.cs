using AutoMapper;
using Books.Application.DTOs.AuthorDto;
using Books.Application.DTOs.BookDTOS;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Mapping
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorCreateDto, AuthorEntity>();
                

            CreateMap<AuthorEntity, AuthorReadDto>()
                .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.Books.Select(b => b.Id)));
        }
    }
}
