using ECommerceApp.Application.Services;
using ECommerceApp.Domain.Entities.BasketAggregate;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Controllers
{
    [ApiController]
    [Route("api/baskets")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket([FromQuery] string id)
        {
            var basket = await _basketService.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBasket([FromBody] CustomerBasket basket)
        {
            var savedBasket = await _basketService.CreateOrUpdateBasketAsync(basket);
            if (savedBasket is null)
                return BadRequest("We couldn't save your basket. Please try again.");
            return Ok(savedBasket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasket(string id)
            => Ok(await _basketService.DeleteBasketAsync(id));
    }
}
