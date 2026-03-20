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
    public class DeleteCityHandler : IRequestHandler<DeleteCityCommand, bool>
    {
        private LibraryDbContext _context;
        public DeleteCityHandler(LibraryDbContext context)
        {
            _context = context;
            
        }
        public async Task<bool> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _context.Set<CityEntity>().FindAsync(request.id);
            if (city == null) return false;
            _context.Remove(city);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
