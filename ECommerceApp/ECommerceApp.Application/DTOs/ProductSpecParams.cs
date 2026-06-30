namespace ECommerceApp.Application.DTOs
{
   
    public class ProductSpecParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }

        public string? Sort { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
