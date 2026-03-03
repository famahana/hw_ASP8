using AutoMapper;
using Books.Application.DTOs.AuthorDto;
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
    public class AuthorService:IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int?> CreateAuthorAsync(AuthorCreateDto author)
        {
            var authorEntity = _mapper.Map<AuthorEntity>(author);
            return await _repository.AddAuthorAsync(authorEntity);
        }

        public async Task<int?> DeleteAuthorAsync(int AuthorId)
        {
            return await _repository.DeleteAuthorAsync(AuthorId);
        }

        public async Task<ICollection<AuthorReadDto>> getAllAuthorAsync()
        {
            var authors = await _repository.getAllAuthorAsync();
            return _mapper.Map<ICollection<AuthorReadDto>>(authors);
        }

        public async Task<AuthorReadDto?> GetAuthorByIdAsync(int id)
        {
            return _mapper.Map<AuthorReadDto>(await _repository.GetAuthorByIdAsync(id));
        }

        public async Task<int?> UpdateAuthorAsync(AuthorCreateDto author, int id)
        {
            var authorEntity = _mapper.Map<AuthorEntity>(author);
            return await _repository.UpdateAuthorAsync(id, authorEntity);
        }
    }
}
