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
        private readonly ICacheService _cacheService;
        public AuthorService(IAuthorRepository repository, IMapper mapper, ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<int?> CreateAuthorAsync(AuthorCreateDto author, CancellationToken cancellation)
        {

            await _cacheService.RemoveAsync("Authors");
            var authorEntity = _mapper.Map<AuthorEntity>(author);
            return await _repository.AddAuthorAsync(authorEntity,cancellation);
        }

        public async Task<int?> DeleteAuthorAsync(int AuthorId)
        {
            await _cacheService.RemoveAsync("Authors");
            return await _repository.DeleteAuthorAsync(AuthorId);
        }

        public async Task<ICollection<AuthorReadDto>> getAllAuthorAsync(CancellationToken cancellation)
        {
            var cache = await _cacheService.GetAsync<ICollection<AuthorReadDto>>("Authors");
            if(cache == null)
            {
                var authors = await _repository.getAllAuthorAsync(cancellation);
                cache = _mapper.Map<ICollection<AuthorReadDto>>(authors);
                await _cacheService.SetAsync("Authors", cache);
               
            }
            return cache;

           
        }

        public async Task<AuthorReadDto?> GetAuthorByIdAsync(int id)
        {
            return _mapper.Map<AuthorReadDto>(await _repository.GetAuthorByIdAsync(id));
        }

        public async Task<int?> UpdateAuthorAsync(AuthorCreateDto author, int id)
        {
            await _cacheService.RemoveAsync("Authors");
            var authorEntity = _mapper.Map<AuthorEntity>(author);
            return await _repository.UpdateAuthorAsync(id, authorEntity);
        }
    }
}
//2) Час життя кеша винести у appsetting.json та читати звідти
