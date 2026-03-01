using Books.Application.Interfaces.Repositories;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly LibraryDbContext _libraryDbContext;
        public GenreRepository(LibraryDbContext context)
        {
            _libraryDbContext = context;
        }
        public async Task<int?> AddGenreAsync(GenreEntity genre)
        {
            _libraryDbContext.Genres.Add(genre);
            await _libraryDbContext.SaveChangesAsync();
            return genre.Id;
        }

        public async Task<int?> DeleteGenreAsync(int GenreId)
        {
            var genre = await _libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == GenreId);
            if (genre == null)
            {
                return null;
            }
            _libraryDbContext.Genres.Remove(genre);
            await _libraryDbContext.SaveChangesAsync();
            return genre.Id;
        }

        public async Task<ICollection<GenreEntity>> getAllGenreAsync()
        {
            return await _libraryDbContext.Genres.ToListAsync();
        }

        public async Task<GenreEntity> GetGenreByIdAsync(int id)
        {
            return await _libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<int?> UpdateGenreAsync(int GenreId, GenreEntity genre)
        {
            var genres = await _libraryDbContext.Genres.FirstOrDefaultAsync(g => g.Id == GenreId);
            if (genres == null)
            {
                return null;
            }
            genres.Title = genre.Title;
            await _libraryDbContext.SaveChangesAsync();
            return genres.Id;
        }
    }
}
