using Books.Application.DTOs.GenreDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services
{
    public interface IGenreService
    {
        Task<ICollection<GenreReadDto>> getAllGenreAsync();
        Task<GenreReadDto> GetGenreByIdAsync(int id);
        Task<int?> AddGenreAsync(GenreCreateDto genre);
        Task<int?> UpdateGenreAsync(int GenreId, GenreCreateDto genre);
        Task<int?> DeleteGenreAsync(int GenreId);
    }
}
