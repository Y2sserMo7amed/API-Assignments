namespace ECommerceApp.Application.DTOs
{
    public class CreateOrderDto
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public OrderAddressDto ShippingAddress { get; set; } = default!;
    }
}
