using Books.Application.Interfaces.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Helpers
{
    public class ImageStorage : IImageStorage
    {
        public async Task<string?> SaveImageAsync(IFormFile file)
        {
            if(file == null)
            {
                return null;
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images",
                fileName);
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return "/images/"+fileName;
        }
    }
}
