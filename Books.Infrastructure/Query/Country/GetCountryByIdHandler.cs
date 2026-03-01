using Books.Domain.Entities;
using Books.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Query.Country
{
    public class GetCountryByIdHandler : IRequestHandler<GetCountryByIdQuery, CountryEntity?>
    {
        private LibraryDbContext _context;
        public GetCountryByIdHandler(LibraryDbContext context)
        {
            _context= context;
        }
        public async Task<CountryEntity?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Id == request.id);
        }
    }
}
