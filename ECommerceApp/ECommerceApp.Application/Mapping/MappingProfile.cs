using AutoMapper;
using ECommerceApp.Application.DTOs;
using ECommerceApp.Domain.Entities.Products;

namespace ECommerceApp.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
