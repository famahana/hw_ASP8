using Books.Application.DTOs.AuthorDto;
using Books.Application.DTOs.BookDTOS;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task<ICollection<AuthorReadDto>> getAllAuthorAsync(CancellationToken cancellation);
        Task<AuthorReadDto?> GetAuthorByIdAsync(int id);
        Task<int?> CreateAuthorAsync(AuthorCreateDto author,CancellationToken cancellation);
        Task<int?> UpdateAuthorAsync(AuthorCreateDto author, int id);
        Task<int?> DeleteAuthorAsync(int AuthorId);

    }
}
