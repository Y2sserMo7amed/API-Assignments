using System.Text.Json;
using ECommerceApp.Domain.Contracts;
using ECommerceApp.Domain.Entities.BasketAggregate;
using StackExchange.Redis;

namespace ECommerceApp.Infrastructure.Repositories
{
    internal class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            try
            {
                var data = await _database.StringGetAsync(basketId);
                return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data!);
            }
            catch (RedisConnectionException) { return null; }
        }

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, int ttlSeconds)
        {
            try
            {
                var json = JsonSerializer.Serialize(basket);
                var saved = await _database.StringSetAsync(basket.Id, json, TimeSpan.FromSeconds(ttlSeconds));
                return saved ? basket : null;
            }
            catch (RedisConnectionException) { return null; }
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            try { return await _database.KeyDeleteAsync(basketId); }
            catch (RedisConnectionException) { return false; }
        }
    }
}
