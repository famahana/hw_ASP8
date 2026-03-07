using Books.Application.Interfaces.Services;
using Books.Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Services
{
    
    public class MemoryCacheService:ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeToExpireCache _timeToExpireCache;
        public MemoryCacheService(IMemoryCache memoryCache, IOptions<TimeToExpireCache> expireOptions)
        {
            _memoryCache = memoryCache;
            _timeToExpireCache = expireOptions.Value;
        }

        public Task<T?> GetAsync<T>(string key)
        {
            if (_memoryCache.TryGetValue(key, out T value))
            {
                return Task.FromResult<T?>(value);
            }
            return Task.FromResult<T?>(default);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }
        
        public Task SetAsync<T>(string key, T value)
        {
            int time = _timeToExpireCache.Time;
            var optionts = new MemoryCacheEntryOptions
            {
                
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(time)
            };
            _memoryCache.Set(key,value, optionts);
            return Task.CompletedTask;
            
        }
    }
}
