using AutoMapper;
using Books.Application.DTOs.BookDTOS;
using Books.Application.DTOs.GenreDto;
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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GenreService(IGenreRepository repository, IMapper mapper,ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<int?> AddGenreAsync(GenreCreateDto genre)
        {
            await _cacheService.RemoveAsync("Genres");
            var genreEntity = _mapper.Map<GenreEntity>(genre);
            return await _repository.AddGenreAsync(genreEntity);

        }

        public async Task<int?> DeleteGenreAsync(int GenreId)
        {
            await _cacheService.RemoveAsync("Genres");
            return await _repository.DeleteGenreAsync(GenreId);
        }

        public async Task<ICollection<GenreReadDto>> getAllGenreAsync()
        {
            var cache = await _cacheService.GetAsync<ICollection<GenreReadDto>>("Genres");
            if (cache == null)
            {
                var genres = await _repository.getAllGenreAsync();
                cache = _mapper.Map<ICollection<GenreReadDto>>(genres);
                await _cacheService.SetAsync("Genres", cache);
            }
            return cache;
        }

        public async Task<GenreReadDto> GetGenreByIdAsync(int id)
        {
            return _mapper.Map<GenreReadDto>(await _repository.GetGenreByIdAsync(id));

        }

        public async Task<int?> UpdateGenreAsync(int GenreId, GenreCreateDto genre)
        {
            await _cacheService.RemoveAsync("Genres");
            var genreEntity = _mapper.Map<GenreEntity>(genre);
            return await _repository.UpdateGenreAsync(GenreId, genreEntity);
        }
    }
}
