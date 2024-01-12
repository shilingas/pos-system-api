using static pos_system.ProductService.Products.ProductModel;

namespace pos_system.ProductService.Products
{
    public class ProductPostRequestModel
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryType { get; set; }
    }
}
