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
        Task<AuthorEntity?> GetAuthorByIdAsync(int id);
        Task<int?> AddAuthorAsync(AuthorEntity author);
        Task<int?> UpdateAuthorAsync(int authorId, AuthorEntity updatedAuthor);
        Task<int?> DeleteAuthorAsync(int AuthorId);


    }
}
