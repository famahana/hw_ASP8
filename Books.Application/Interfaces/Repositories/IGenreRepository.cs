using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        Task<ICollection<GenreEntity>> getAllGenreAsync();
        Task<GenreEntity> GetGenreByIdAsync(int id);
        Task<int?> AddGenreAsync(GenreEntity genre, ICollection<int>? bookIds);
        Task<bool?> UpdateGenreAsync(int GenreId, GenreEntity genre,ICollection<int>? bookIds);
        Task<bool?> DeleteGenreAsync(int GenreId);
    }
}
