using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Books.Domain;
using System.Threading.Tasks;
using Books.Domain.Entities;

namespace Books.Application.Interfaces.Repositories
{
    public interface IBookRepository
    {
        /// <summary>
        /// Получение всех книг из бд
        /// </summary>
        /// <returns></returns>
        Task<ICollection<BookEntity>> getAllBooksAsync();
        Task<BookEntity> GetBookByIdAsync(int id);
        Task<int?> AddBookAsync(BookEntity book, ICollection<int>? authorIds);
        Task<ICollection<BookEntity>> GetChunkBooksAsync();
    }
}
