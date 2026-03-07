using Books.Application.Interfaces.Services;
using Books.Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.Infrastructure.Services
{
    public class RedisCachingService : ICacheService
    {
        private readonly IDatabase _database;
        private readonly TimeToExpireCache _timeToExpireCache;
        public RedisCachingService(IConnectionMultiplexer multiplexer, IOptions<TimeToExpireCache> expireOptions)
        {
            _database = multiplexer.GetDatabase();
            _timeToExpireCache = expireOptions.Value;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if(value.IsNullOrEmpty)
            {
                return default(T?);
            }
            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, json, TimeSpan.FromMinutes(_timeToExpireCache.Time));
        }
    }
}
