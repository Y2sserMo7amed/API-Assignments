using ECommerceApp.Infrastructure.Data;

namespace ECommerceApp.API.Extensions
{
  
    public static class WebApplicationExtensions
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            await StoreContextSeed.SeedAsync(dbContext, app.Environment.ContentRootPath);
        }
    }
}
