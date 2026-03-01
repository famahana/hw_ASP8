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
        public async Task<int?> AddAuthorAsync(AuthorEntity author)
        {
            
            await _libraryDbContext.Authors.AddAsync(author);  
            await _libraryDbContext.SaveChangesAsync();
            return author.Id;
            
        }

        public async Task<int?> DeleteAuthorAsync(int AuthorId)
        { 
            var author = await _libraryDbContext.Authors.FirstOrDefaultAsync(a => a.Id == AuthorId);
            if (author == null)
            {
                return null;
            }
            _libraryDbContext.Authors.Remove(author);
            await _libraryDbContext.SaveChangesAsync();
            return author.Id;
        }

        public async Task<ICollection<AuthorEntity>> getAllAuthorAsync()
        {
            return await _libraryDbContext.Authors.ToListAsync();
               
        }

        public async Task<AuthorEntity> GetAuthorByIdAsync(int id)
        {
            return await _libraryDbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
                
        }

        public async Task<int?> UpdateAuthorAsync(int authorId, AuthorEntity updatedAuthor)
        {
            var author = await _libraryDbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
            if(author == null)
            {
                return null;
            }
            author.Name = updatedAuthor.Name;
            author.Surname = updatedAuthor.Surname;
            await _libraryDbContext.SaveChangesAsync();
            return author.Id;
        }
    }
}
