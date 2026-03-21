using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Command.Country
{
    public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, CountryEntity>
    {
        private LibraryDbContext _context;
        private readonly ICacheService _cacheService;
        public CreateCountryHandler(LibraryDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task<CountryEntity> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveAsync("Country");
            var country = new CountryEntity();
            country.Name = request.name;
            await _context.AddAsync(country);
            await _context.SaveChangesAsync();
            return country;
        }
            
        
    }

}
