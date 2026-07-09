namespace ECommerceApp.Domain.Entities.BasketAggregate
{
  
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;

        
        public List<BasketItem> Items { get; set; } = new();

        
        public CustomerBasket()
        {
        }

        
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
