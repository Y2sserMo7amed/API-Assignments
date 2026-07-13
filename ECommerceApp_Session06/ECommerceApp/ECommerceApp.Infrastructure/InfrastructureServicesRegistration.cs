using ECommerceApp.Application.Services;
using ECommerceApp.Domain.Contracts;
using ECommerceApp.Infrastructure.Data;
using ECommerceApp.Infrastructure.Identity;
using ECommerceApp.Infrastructure.Repositories;
using ECommerceApp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StackExchange.Redis;

namespace ECommerceApp.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            
            services.AddSingleton<IConnectionMultiplexer>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis")!);
                options.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect(options);
            });

            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>();

            return services;
        }
    }
}
