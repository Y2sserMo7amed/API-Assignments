using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Entities.Products
{
  
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}
