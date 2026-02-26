using AutoMapper;
using Books.Application.Interfaces.Repositories;
using Books.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services
{
    public class AuthorService:IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
