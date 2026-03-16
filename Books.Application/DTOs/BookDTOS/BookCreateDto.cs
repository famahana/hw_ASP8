using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.DTOs.BookDTOS
{
    public class BookCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public ICollection<int>? AuthorIds { get; set; }
        public int GenreId { get; set; }
        public int Price { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
