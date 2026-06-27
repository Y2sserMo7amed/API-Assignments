using AutoMapper;
using ECommerceApp.Application.DTOs;
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

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var repository = _unitOfWork.GetRepository<Product, int>();
            var products = await repository.GetAllAsync();

           
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var repository = _unitOfWork.GetRepository<Product, int>();
            var product = await repository.GetByIdAsync(id);

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
