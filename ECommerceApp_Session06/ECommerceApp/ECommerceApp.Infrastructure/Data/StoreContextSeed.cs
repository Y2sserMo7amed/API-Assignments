using System.Text.Json;
using ECommerceApp.Domain.Entities.OrderAggregate;
using ECommerceApp.Domain.Entities.Products;

namespace ECommerceApp.Infrastructure.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext dbContext, string contentRootPath)
        {
          
            if (!dbContext.ProductBrands.Any())
            {
                var data = await File.ReadAllTextAsync(
                    Path.Combine(contentRootPath, "Data", "SeedData", "brands.json"));
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                if (brands?.Count > 0) { dbContext.ProductBrands.AddRange(brands); await dbContext.SaveChangesAsync(); }
            }

            if (!dbContext.ProductTypes.Any())
            {
                var data = await File.ReadAllTextAsync(
                    Path.Combine(contentRootPath, "Data", "SeedData", "types.json"));
                var types = JsonSerializer.Deserialize<List<ProductType>>(data);
                if (types?.Count > 0) { dbContext.ProductTypes.AddRange(types); await dbContext.SaveChangesAsync(); }
            }

            if (!dbContext.Products.Any())
            {
                var data = await File.ReadAllTextAsync(
                    Path.Combine(contentRootPath, "Data", "SeedData", "products.json"));
                var products = JsonSerializer.Deserialize<List<Product>>(data);
                if (products?.Count > 0) { dbContext.Products.AddRange(products); await dbContext.SaveChangesAsync(); }
            }

            if (!dbContext.DeliveryMethods.Any())
            {
                var data = await File.ReadAllTextAsync(
                    Path.Combine(contentRootPath, "Data", "SeedData", "deliverymethods.json"));
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);
                if (methods?.Count > 0) { dbContext.DeliveryMethods.AddRange(methods); await dbContext.SaveChangesAsync(); }
            }
        }
    }
}
