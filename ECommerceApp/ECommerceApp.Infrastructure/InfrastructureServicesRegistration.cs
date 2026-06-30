using ECommerceApp.Domain.Contracts;
using ECommerceApp.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ECommerceApp.Infrastructure
{
   
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

           
            services.AddSingleton<IConnectionMultiplexer>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var redisConnectionString = configuration.GetConnectionString("Redis");

                var redisOptions = ConfigurationOptions.Parse(redisConnectionString!);

                
                redisOptions.AbortOnConnectFail = false;

                return ConnectionMultiplexer.Connect(redisOptions);
            });

            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}
