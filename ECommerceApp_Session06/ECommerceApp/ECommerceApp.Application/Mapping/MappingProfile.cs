using AutoMapper;
using ECommerceApp.Application.DTOs;
using ECommerceApp.Domain.Entities.OrderAggregate;
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

            CreateMap<OrderAddressDto, OrderAddress>();
            CreateMap<OrderAddress, OrderAddressDto>();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductItemOrdered.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductItemOrdered.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.ProductItemOrdered.PictureUrl));

            CreateMap<DeliveryMethod, DeliveryMethodDto>();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.DeliveryMethodPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));
        }
    }
}
