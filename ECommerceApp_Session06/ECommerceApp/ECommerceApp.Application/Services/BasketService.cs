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

        public Task<CustomerBasket?> GetBasketAsync(string basketId)
            => _basketRepository.GetBasketAsync(basketId);

        public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket)
            => _basketRepository.CreateOrUpdateBasketAsync(basket, BasketTtlInSeconds);

        public Task<bool> DeleteBasketAsync(string basketId)
            => _basketRepository.DeleteBasketAsync(basketId);
    }
}
