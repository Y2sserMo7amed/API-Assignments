namespace ECommerceApp.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; } = default!;
        public OrderAddressDto ShippingAddress { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public decimal DeliveryMethodPrice { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
