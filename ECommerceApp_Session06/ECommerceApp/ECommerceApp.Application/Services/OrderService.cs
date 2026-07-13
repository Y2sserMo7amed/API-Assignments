using AutoMapper;
using ECommerceApp.Application.DTOs;
using ECommerceApp.Application.Specifications;
using ECommerceApp.Domain.Contracts;
using ECommerceApp.Domain.Entities.OrderAggregate;
using ECommerceApp.Domain.Entities.Products;

namespace ECommerceApp.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto?> CreateOrderAsync(string userEmail, CreateOrderDto createOrderDto)
        {
            var shippingAddress = _mapper.Map<OrderAddress>(createOrderDto.ShippingAddress);

            var basket = await _basketRepository.GetBasketAsync(createOrderDto.BasketId);
            if (basket == null) return null;

            
            var orderItems = new List<OrderItem>();
            var productRepo = _unitOfWork.GetRepository<Product, int>();

            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetByIdAsync(item.Id);
                if (product == null) continue;

                var productSnapshot = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productSnapshot, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            if (!orderItems.Any()) return null;

            var deliveryMethodRepo = _unitOfWork.GetRepository<DeliveryMethod, int>();
            var deliveryMethod = await deliveryMethodRepo.GetByIdAsync(createOrderDto.DeliveryMethodId);
            if (deliveryMethod == null) return null;

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            var order = new Order(userEmail, shippingAddress, deliveryMethod, orderItems, subTotal);
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            orderRepo.Add(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(string userEmail)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var spec = new OrdersForUserSpecification(userEmail);
            var orders = await orderRepo.GetAllWithSpecAsync(spec);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id, string userEmail)
        {
            var orderRepo = _unitOfWork.GetRepository<Order, int>();
            var spec = new OrdersForUserSpecification(id, userEmail);
            var order = await orderRepo.GetEntityWithSpecAsync(spec);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var repo = _unitOfWork.GetRepository<DeliveryMethod, int>();
            var methods = await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodDto>>(methods);
        }
    }
}
