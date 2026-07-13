using ECommerceApp.Domain.Entities.OrderAggregate;
using ECommerceApp.Domain.Specifications;

namespace ECommerceApp.Application.Specifications
{
    public class OrdersForUserSpecification : BaseSpecification<Order, int>
    {
        public OrdersForUserSpecification(string userEmail)
            : base(o => o.UserEmail == userEmail)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

       
        public OrdersForUserSpecification(int id, string userEmail)
            : base(o => o.Id == id && o.UserEmail == userEmail)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
