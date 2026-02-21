using Books.Application.DTOs.BookDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<int?> CreateBookAsync(BookCreateDto dto);
        Task<BookReadDto?> GetBookByIdAsync(int id);
        Task<ICollection<BookReadDto>> GetAllBooksAsync();
        Task<ICollection<BookReadDto>> GetChunkBooksAsync();

    }
}
