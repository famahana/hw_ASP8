using AutoMapper;
using Books.Application.DTOs.AuthorDto;
using Books.Application.DTOs.GenreDto;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using Books.Application.Mapping;
using Books.Application.Services;
using Books.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static StackExchange.Redis.Role;
namespace Books.Tests;

public class GenreServiceTest
{
    private readonly GenreService _service;
    private readonly Mock<IGenreRepository> _genreRepoMock;
    private readonly Mock<ICacheService> _cacheMock;
    private readonly IMapper _mapper;
    private readonly ILoggerFactory _loggerFactory;


    public GenreServiceTest()
    {
        _genreRepoMock = new Mock<IGenreRepository>();
        _cacheMock = new Mock<ICacheService>();

        _loggerFactory = LoggerFactory.Create(builder => { });

        // Налаштовуємо AutoMapper для простого тесту
        // Створюємо конфігурацію і додаємо твій профіль

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<GenreProfile>();
        }, _loggerFactory);

        // (опціонально) перевіряємо, що всі мапінги валідні
        config.AssertConfigurationIsValid();
        _mapper = new Mapper(config);
        _service = new GenreService(_genreRepoMock.Object, _mapper, _cacheMock.Object);
    }

    [Fact]
    public async Task GetAllGenreAsync_ShouldReturnGenre_FromCache_WhenCacheExists()
    {
        // Arrange: кеш вже містить авторів
        var cachedGenres = new List<GenreReadDto>
        {
            new GenreReadDto { Id = 1, Title = "Genre1" }
        };

        _cacheMock.Setup(c => c.GetAsync<ICollection<GenreReadDto>>("Genres"))
                  .ReturnsAsync(cachedGenres);

        // Act
        var result = await _service.getAllGenreAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result); //колекція містить лише 1 елемент
        Assert.Equal("Genre1", result.First().Title);

        // Репозиторій не має викликатися, бо кеш є
        _genreRepoMock.Verify(r => r.getAllGenreAsync(),Times.Never);
    }


    [Fact]
    public async Task GetAllGenreAsync_CacheEmpty_FetchesFromRepositoryAndSetsCache()
    {
        // Arrange
        var genresFromRepo = new List<GenreEntity>
        {
            new GenreEntity { Id = 1, Title = "Genre1" },
            new GenreEntity { Id = 2, Title = "Genre2" }
        };

        // Кеш порожній
        _cacheMock.Setup(c => c.GetAsync<ICollection<GenreReadDto>>("Genres"))
                  .ReturnsAsync((ICollection<GenreReadDto>)null);

        // Репозиторій повертає дані
        _genreRepoMock.Setup(r => r.getAllGenreAsync())
                       .ReturnsAsync(genresFromRepo);

        var service = new GenreService(_genreRepoMock.Object, _mapper, _cacheMock.Object);

        // Act
        var result = await service.getAllGenreAsync();

        // Assert
        Assert.Equal(2, result.Count); // перевіряємо, що повернуло два елементи
        Assert.Contains(result, a => a.Title == "Genre1");
        Assert.Contains(result, a => a.Title == "Genre2");

        // Перевіряємо, що кеш було встановлено (TimesOnce перевірка, щоб метод був викликаний 1 раз)
        _cacheMock.Verify(c => c.SetAsync("Genres", It.IsAny<ICollection<GenreReadDto>>()), Times.Once);
    }
    [Fact]
    public async Task GetGenreByIdAsync_ShouldReturnGenre_WhenExists()
    {
        
        var genreId = 1;

        var genreEntity = new GenreEntity
        {
            Id = genreId,
            Title = "Action"
        };

        _genreRepoMock.Setup(r => r.GetGenreByIdAsync(genreId))
                      .ReturnsAsync(genreEntity);

      
        var result = await _service.GetGenreByIdAsync(genreId);

       
        Assert.NotNull(result);
        Assert.Equal(genreId, result.Id);
        Assert.Equal("Action", result.Title);

        _genreRepoMock.Verify(r => r.GetGenreByIdAsync(genreId), Times.Once);
    }
}