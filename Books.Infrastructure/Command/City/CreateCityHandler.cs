using Books.Domain.Entities;
using Books.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Command.City
{
    public class CreateCityHandler:IRequestHandler<CreateCityCommand,CityEntity>
    {
        private LibraryDbContext _context;
        public CreateCityHandler(LibraryDbContext context)
        {
            _context = context;
            
        }

        public async Task<CityEntity> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var city = new CityEntity()
            {
                Name = request.Name,
                CountryId = request.CountryId
            };
            _context.Set<CityEntity>().Add(city);
            await _context.SaveChangesAsync(cancellationToken);
            return city;

            
        }
    }
}
