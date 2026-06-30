using System.Text.Json;
using ECommerceApp.Domain.Entities.Products;

namespace ECommerceApp.Infrastructure.Data
{
   
    public static class StoreContextSeed
    {
        
        public static async Task SeedAsync(StoreDbContext dbContext, string contentRootPath)
        {
           
            if (!dbContext.ProductBrands.Any())
            {
                var brandsData = await File.ReadAllTextAsync(
                    Path.Combine(contentRootPath, "Data", "SeedData", "brands.json"));

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands != null && brands.Count > 0)
                {
                    dbContext.ProductBrands.AddRange(brands);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.ProductTypes.Any())
            {
                var typesData = await File.ReadAllTextAsync(
                    Path.Combine(contentRootPath, "Data", "SeedData", "types.json"));

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types != null && types.Count > 0)
                {
                    dbContext.ProductTypes.AddRange(types);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(
                    Path.Combine(contentRootPath, "Data", "SeedData", "products.json"));

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products != null && products.Count > 0)
                {
                    dbContext.Products.AddRange(products);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
