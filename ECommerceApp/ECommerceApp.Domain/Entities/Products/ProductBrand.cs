using ECommerceApp.Domain.Common;

namespace ECommerceApp.Domain.Entities.Products
{
   
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}
