using Books.Application.Interfaces.Services;
using Books.Domain.Entities;
using Books.Infrastructure.Data;
using Books.Infrastructure.Query.City;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Query.Country
{
    public class GetAllCountryHandler : IRequestHandler<GetAllCountryQuery, ICollection<CountryEntity>>
    {
        private LibraryDbContext _context;
        private readonly ICacheService _cacheService;
        public GetAllCountryHandler(LibraryDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
            
        }
        public async Task<ICollection<CountryEntity>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            var cache = await _cacheService.GetAsync<ICollection<CountryEntity>>("Country");
            if (cache == null)
            {
                var countries = await _context.Countries.ToListAsync(cancellationToken);
                cache = countries;
                await _cacheService.SetAsync("Country", cache);

            }
            return cache;

        }

    }
}
