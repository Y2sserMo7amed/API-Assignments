using ECommerceApp.Domain.Entities.BasketAggregate;

namespace ECommerceApp.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, int ttlSeconds);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
