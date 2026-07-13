using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Entities.OrderAggregate
{
    public class Order : BaseEntity<int>
    {
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public OrderAddress ShippingAddress { get; set; } = default!;

        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal SubTotal { get; set; }

        public decimal Total => SubTotal + (DeliveryMethod?.Price ?? 0);

        public Order() { }

        public Order(string userEmail, OrderAddress shippingAddress, DeliveryMethod deliveryMethod,
                     ICollection<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }
    }
}
