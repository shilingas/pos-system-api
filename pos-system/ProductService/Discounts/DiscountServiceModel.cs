using System.Diagnostics.CodeAnalysis;

namespace pos_system.ProductService.Discounts
{
    public class DiscountServiceModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? ServiceId { get; set; }
        public string? DiscountId { get; set; }
        public string? Name { get; set; }
    }
}
