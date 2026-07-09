using System.Text.Json;
using ECommerceApp.Application.Services;
using StackExchange.Redis;

namespace ECommerceApp.Infrastructure.Services
{
  
    internal class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<string?> GetAsync(string key)
        {
            try
            {
                var data = await _database.StringGetAsync(key);

                if (data.IsNullOrEmpty)
                {
                    return null;
                }

                
                return data.ToString();
            }
            catch (RedisConnectionException)
            {
                
                return null;
            }
        }

        public async Task SetAsync(string key, object value, int durationInSeconds)
        {
            try
            {
                var json = JsonSerializer.Serialize(value);
                await _database.StringSetAsync(key, json, TimeSpan.FromSeconds(durationInSeconds));
            }
            catch (RedisConnectionException)
            {
             
            }
        }
    }
}
