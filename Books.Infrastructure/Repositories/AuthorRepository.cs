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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _libraryDbContext;
        public AuthorRepository(LibraryDbContext context)
        {
            _libraryDbContext = context;
        }
        private async Task <ICollection<BookEntity>> GetBooksAsync(ICollection<int>bookIds)
        {
            var books = await _libraryDbContext.Books.Where(b=>bookIds.Contains(b.Id)).ToListAsync();
            if(books.Count != bookIds.Count)
            {
                throw new Exception("Some books not found");
            }
            return books;
        }
        public async Task<int?> AddAuthorAsync(AuthorEntity author, ICollection<int>? bookIds)
        {
            if(bookIds != null)
            {
                author.Books = await GetBooksAsync(bookIds);
            }
            _libraryDbContext.Authors.Add(author);  
            await _libraryDbContext.SaveChangesAsync();
            return author.Id;
            
        }

        public async Task<bool?> DeleteAuthorAsync(int AuthorId)
        {
            var author = await _libraryDbContext.Authors
                .Include(a => a.Books)
                .SingleOrDefaultAsync(a => a.Id == AuthorId);
            if(author == null)
            {
                return null;
            }
            author.Books.Clear();
            _libraryDbContext.Authors.Remove(author);
            await _libraryDbContext .SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<AuthorEntity>> getAllAuthorAsync()
        {
            return await _libraryDbContext.Authors
               .Include(b => b.Books)
               .ToListAsync();
        }

        public async Task<AuthorEntity> GetAuthorByIdAsync(int id)
        {
            return await _libraryDbContext.Authors
                .Include(a => a.Books)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool?> UpdateAuthorAsync(int authorId, AuthorEntity updatedAuthor, ICollection<int>? bookIds)
        {
            var author = await _libraryDbContext.Authors
                .Include(a => a.Books)
                .SingleOrDefaultAsync(a => a.Id == authorId);
            if(author == null)
            {
                return null;
            }
            author.Name = updatedAuthor.Name;
            author.Surname = updatedAuthor.Surname;
            if(bookIds != null)
            {
                var books = await GetBooksAsync(bookIds);
                author.Books = books;
            }
            await _libraryDbContext.SaveChangesAsync();
            return true;
        }
    }
}
