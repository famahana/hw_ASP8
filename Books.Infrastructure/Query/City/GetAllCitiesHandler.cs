using Books.Domain.Entities;
using Books.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Query.City
{
    public class GetAllCitiesHandler:IRequestHandler<GetAllCitiesQuery,ICollection<CityEntity>>
    {
        private LibraryDbContext _context;
        public GetAllCitiesHandler(LibraryDbContext context)
        {
            _context = context;
            
            
        }

        public async Task<ICollection<CityEntity>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<CityEntity>()
                .Include(c=>c.Country)
                .ToListAsync(cancellationToken);
        }
    }
}
