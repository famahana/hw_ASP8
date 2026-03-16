using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Interfaces.Helpers
{
    public interface IImageStorage
    {
        Task<string?> SaveImageAsync(IFormFile file);
    }
}
