using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrdered ProductItemOrdered { get; set; } = default!;

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public OrderItem() { }

        public OrderItem(ProductItemOrdered productItemOrdered, decimal price, int quantity)
        {
            ProductItemOrdered = productItemOrdered;
            Price = price;
            Quantity = quantity;
        }
    }
}
