using AutoMapper;
using Books.Application.DTOs.GenreDto;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Domain.Entities;

namespace Books.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<int?> AddGenreAsync(GenreCreateDto genre)
        {
            var genreEntity = _mapper.Map<GenreEntity>(genre);
            return await _repository.AddGenreAsync(genreEntity);

        }

        public async Task<int?> DeleteGenreAsync(int GenreId)
        {
            return await _repository.DeleteGenreAsync(GenreId);
        }

        public async Task<ICollection<GenreReadDto>> getAllGenreAsync()
        {
            var genres = await _repository.getAllGenreAsync();
            return _mapper.Map<ICollection<GenreReadDto>>(genres);
        }

        public async Task<GenreReadDto> GetGenreByIdAsync(int id)
        {
            return _mapper.Map<GenreReadDto>(await _repository.GetGenreByIdAsync(id));

        }

        public async Task<int?> UpdateGenreAsync(int GenreId, GenreCreateDto genre)
        {
            var genreEntity = _mapper.Map<GenreEntity>(genre);
            return await _repository.UpdateGenreAsync(GenreId, genreEntity);
        }
    }
}
