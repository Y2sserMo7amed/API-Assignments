using ECommerceApp.Domain.Contracts;
using ECommerceApp.Domain.Entities.BasketAggregate;

namespace ECommerceApp.Application.Services
{
    public class BasketService : IBasketService
    {
        
        private const int BasketTtlInSeconds = 60 * 60 * 24 * 3; 

        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            return await _basketRepository.GetBasketAsync(basketId);
        }

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket)
        {
            return await _basketRepository.CreateOrUpdateBasketAsync(basket, BasketTtlInSeconds);
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
}
