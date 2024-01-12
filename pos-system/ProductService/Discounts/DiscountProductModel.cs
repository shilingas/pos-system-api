using System.Diagnostics.CodeAnalysis;

namespace pos_system.ProductService.Discounts
{
    public class DiscountProductModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public string? DiscountId { get; set; }
        public string? Name { get; set; }
    }
}
