using AutoMapper;
using ECommerceApp.Application.DTOs;
using ECommerceApp.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;

namespace ECommerceApp.Application.Mapping
{
   
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }

           
            var baseUrl = _configuration["BaseUrl"];
            return $"{baseUrl}/{source.PictureUrl}";
        }
    }
}
