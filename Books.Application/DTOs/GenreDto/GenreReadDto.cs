using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.GenreDto
{
    public class GenreReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public ICollection<int>? BookIds { get; set; }
        public GenreReadDto()
        {
            
        }
        public GenreReadDto(GenreEntity genre)
        {
            Id = genre.Id;
            Title = genre.Title;
            BookIds = genre.Books == null ? [] : genre.Books.Select(book => book.Id).ToList();

        }
    }
}
