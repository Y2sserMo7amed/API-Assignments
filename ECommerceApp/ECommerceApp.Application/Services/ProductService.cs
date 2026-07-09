using AutoMapper;
using ECommerceApp.Application.DTOs;
using ECommerceApp.Application.Specifications;
using ECommerceApp.Domain.Contracts;
using ECommerceApp.Domain.Entities.Products;

namespace ECommerceApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams specParams)
        {
            var repository = _unitOfWork.GetRepository<Product, int>();

            var spec = new ProductsWithBrandsAndTypesSpecification(specParams);

           
            var products = await repository.GetAllWithSpecAsync(spec);

           
            var totalCount = await repository.CountAsync(spec);

            var data = _mapper.Map<IEnumerable<ProductDto>>(products);

            return new PaginationResponse<ProductDto>
            {
                PageIndex = specParams.PageIndex,
                PageSize = specParams.PageSize,
                Count = totalCount,
                Data = data
            };
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Product, int>();

            var spec = new ProductsWithBrandsAndTypesSpecification(id);
            var product = await repository.GetEntityWithSpecAsync(spec);

            if (product is null)
            {
                return null;
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repository = _unitOfWork.GetRepository<ProductBrand, int>();
            var brands = await repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var repository = _unitOfWork.GetRepository<ProductType, int>();
            var types = await repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDto>>(types);
        }
    }
}
