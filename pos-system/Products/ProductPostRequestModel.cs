using static pos_system.Products.ProductModel;

namespace pos_system.Products
{
    public class ProductPostRequestModel
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryType { get; set; }
    }
}
