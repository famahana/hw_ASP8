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
        
        private async Task<ICollection<BookEntity>> GetBooksAsync(ICollection<int> bookIds)
        {
            var books = await _libraryDbContext.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();
            if (books.Count != bookIds.Count)
            {
                throw new Exception("Some books not found");
            }
            return books;
        }
        public async Task<int?> AddGenreAsync(GenreEntity genre, ICollection<int>? bookIds)
        {
            if (bookIds != null)
            {
                genre.Books = await GetBooksAsync(bookIds);
            }
            _libraryDbContext.Genres.Add(genre);
            await _libraryDbContext.SaveChangesAsync();
            return genre.Id;
        }

        public async Task<bool?> DeleteGenreAsync(int GenreId)
        {
            var genre = await _libraryDbContext.Genres
                .Include(g => g.Books)
                .SingleOrDefaultAsync(g => g.Id == GenreId);
            if (genre == null)
            {
                return null;
            }
            genre.Books.Clear();
            _libraryDbContext.Genres.Remove(genre);
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<GenreEntity>> getAllGenreAsync()
        {
            return await _libraryDbContext.Genres
               .Include(g=>g.Books)
               .ToListAsync();
        }

        public async Task<GenreEntity> GetGenreByIdAsync(int id)
        {
            return await _libraryDbContext.Genres
                .Include(g => g.Books)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool?> UpdateGenreAsync(int GenreId, GenreEntity genre, ICollection<int>? bookIds)
        {
            var genres = await _libraryDbContext.Genres
                .Include(g => g.Books)
                .SingleOrDefaultAsync(g => g.Id == GenreId);
            if (genres == null)
            {
                return null;
            }
            genres.Title = genre.Title;
            if (bookIds != null)
            {
                var books = await GetBooksAsync(bookIds);
                genres.Books = books;
            }
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }
    }
}
