using Books.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Query.Country
{
    public record GetAllCountryQuery:IRequest<ICollection<CountryEntity>>;
    

    
}
