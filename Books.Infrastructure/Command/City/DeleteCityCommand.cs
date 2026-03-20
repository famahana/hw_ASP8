using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Command.City
{
    public record DeleteCityCommand(int id) :IRequest<bool>;
   
}
