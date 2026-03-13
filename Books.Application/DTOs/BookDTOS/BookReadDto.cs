using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.BookDTOS
{
    public class BookReadDto
    {
        public int Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public int GenreId { get; set; }
        public ICollection<int>? AuthorIds { get; set; }
        public int Price { get; set; }
        public BookReadDto()
        {

        }
        public BookReadDto(BookEntity book)
        {
            Id = book.Id;
            Title = book.Title;
            Year = book.Year;
            AuthorIds = book.Authors == null? [] :book.Authors.Select(author => author.Id).ToList();
            GenreId = book.GenreId;
            Price = book.Price;
        }

    }
}
