using AutoMapper;
using Books.Application.DTOs.AuthorDto;
using Books.Application.DTOs.BookDTOS;
using Books.Application.Interfaces.Helpers;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services
{
    public class BookSerice : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IImageStorage _imageStorage;

        public BookSerice(IBookRepository repository, IMapper mapper, ICacheService cacheService,IImageStorage imageStorage)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
            _imageStorage = imageStorage;
        }

        // Створення книги
        public async Task<int?> CreateBookAsync(BookCreateDto dto)
        {
            await _cacheService.RemoveAsync("Books");
            var imagePath = await _imageStorage.SaveImageAsync(dto.ImageUrl);
            var book = _mapper.Map<BookEntity>(dto);
            book.ImageUrl = imagePath;
            return await _repository.AddBookAsync(book, dto.AuthorIds);
        }

        // Отримати книгу по Id
        public async Task<BookReadDto?> GetBookByIdAsync(int id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            if (book == null) return null;
            var dto = _mapper.Map<BookReadDto>(book);
            return dto;
        }

        // Отримати всі книги
        public async Task<ICollection<BookReadDto>> GetAllBooksAsync()
        {
            var cache = await _cacheService.GetAsync<ICollection<BookReadDto>>("Books");
            if(cache == null)
            {
                var books = await _repository.getAllBooksAsync();
                cache = _mapper.Map<ICollection<BookReadDto>>(books);
                await _cacheService.SetAsync("Books", cache);
            }
            return cache;
            
        }

        public Task<ICollection<BookReadDto>> GetChunkBooksAsync()
        {
            throw new NotImplementedException();
        }
       
    }
}
