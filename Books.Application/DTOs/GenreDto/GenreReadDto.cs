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
    }
}
