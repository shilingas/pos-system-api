using System.Diagnostics.CodeAnalysis;

namespace pos_system.ProductService.Products
{
    public class ProductModel
    {
        public enum Category
        {
            Beauty,
            Food,
        }
        [DisallowNull]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? CategoryType { get; set; }
    }
}
