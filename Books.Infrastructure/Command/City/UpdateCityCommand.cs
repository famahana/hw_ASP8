using Books.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Command.City
{
    public record UpdateCityCommand(int id, string Name):IRequest<bool>;

    
   
}
