using ECommerceApp.Domain.Contracts;
using ECommerceApp.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Infrastructure
{
   
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
           
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
