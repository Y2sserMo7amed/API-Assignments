using ECommerceApp.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Data
{
  
    public class StoreDbContext : DbContext
    {
       
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
