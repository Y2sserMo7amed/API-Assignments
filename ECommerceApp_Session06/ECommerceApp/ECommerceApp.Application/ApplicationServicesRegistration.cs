using ECommerceApp.Application.Mapping;
using ECommerceApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
