using AutoMapper;
using Books.Application.DTOs.AuthorDto;
using Books.Application.DTOs.GenreDto;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Mapping
{
    public class GenreProfile:Profile
    {
        public GenreProfile()
        {
            CreateMap<GenreCreateDto, GenreEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.Ignore());


            CreateMap<GenreEntity, GenreReadDto>()
                .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.Books.Select(b => b.Id)));
        }
    }
}
