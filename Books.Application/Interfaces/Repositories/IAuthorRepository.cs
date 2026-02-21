using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<ICollection<AuthorEntity>> getAllAuthorAsync();
        Task<AuthorEntity> GetAuthorByIdAsync(int id);
        Task<int?> AddAuthorAsync(AuthorEntity author, ICollection<int>? bookIds);
        Task<bool?> UpdateAuthorAsync(int authorId, AuthorEntity updatedAuthor, ICollection<int>? bookIds);
        Task<bool?> DeleteAuthorAsync(int AuthorId);
        

    }
}
