using Books.Application.DTOs.BookDTOS;
using Books.Application.Interfaces.Services;
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
        private readonly ICacheService _cacheService;
        public GetCountryByIdHandler(LibraryDbContext context, ICacheService cacheService)
        {
            _context= context;
            _cacheService= cacheService;
        }
        public async Task<CountryEntity?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {                  
               return await _context.Countries.FirstOrDefaultAsync(c => c.Id == request.id);                                     
        }
    }
}
