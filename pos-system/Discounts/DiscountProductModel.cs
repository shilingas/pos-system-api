using System.Diagnostics.CodeAnalysis;

namespace pos_system.Discounts
{
    public class DiscountProductModel
    {
        [DisallowNull]
        public String? Id { get; set; }
        public String? ProductId { get; set; }
        public String? DiscountId { get; set; }
        public String? Name { get; set; }
    }
}
