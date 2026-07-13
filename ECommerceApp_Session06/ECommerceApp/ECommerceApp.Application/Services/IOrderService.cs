using ECommerceApp.Application.DTOs;

namespace ECommerceApp.Application.Services
{
    public interface IOrderService
    {
        Task<OrderDto?> CreateOrderAsync(string userEmail, CreateOrderDto createOrderDto);
        Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(string userEmail);
        Task<OrderDto?> GetOrderByIdAsync(int id, string userEmail);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    }
}
