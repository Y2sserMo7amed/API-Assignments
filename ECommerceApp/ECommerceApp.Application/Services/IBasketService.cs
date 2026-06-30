using ECommerceApp.Domain.Entities.BasketAggregate;

namespace ECommerceApp.Application.Services
{
    public interface IBasketService
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
