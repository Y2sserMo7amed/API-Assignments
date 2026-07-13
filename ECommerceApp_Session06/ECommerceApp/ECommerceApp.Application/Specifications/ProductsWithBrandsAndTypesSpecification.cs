using ECommerceApp.Application.DTOs;
using ECommerceApp.Domain.Entities.Products;
using ECommerceApp.Domain.Specifications;

namespace ECommerceApp.Application.Specifications
{
   
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product, int>
    {
      
        public ProductsWithBrandsAndTypesSpecification(ProductSpecParams specParams)
            : base(p =>
                
                (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value) &&
                (!specParams.TypeId.HasValue || p.TypeId == specParams.TypeId.Value) &&
                (string.IsNullOrEmpty(specParams.Search) ||
                    p.Name.ToLower().Contains(specParams.Search.ToLower())))
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch (specParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }

            var skip = specParams.PageSize * (specParams.PageIndex - 1);
            ApplyPaging(skip, specParams.PageSize);
        }

        public ProductsWithBrandsAndTypesSpecification(int id)
            : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
