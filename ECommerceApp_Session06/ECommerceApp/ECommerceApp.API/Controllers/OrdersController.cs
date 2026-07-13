using ECommerceApp.Application.DTOs;
using ECommerceApp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApp.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]  
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var order = await _orderService.CreateOrderAsync(email, createOrderDto);
            if (order == null)
                return BadRequest("Could not create order. Check basket and delivery method.");

            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var order = await _orderService.GetOrderByIdAsync(id, email);
            return order == null ? NotFound() : Ok(order);
        }

        
        [HttpGet("deliverymethods")]
        [AllowAnonymous]   
        public async Task<IActionResult> GetDeliveryMethods()
            => Ok(await _orderService.GetDeliveryMethodsAsync());
    }
}
