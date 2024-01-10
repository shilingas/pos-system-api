using pos_system.Order;
using System.Diagnostics.CodeAnalysis;

namespace pos_system.Discounts
{
    public class DiscountModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public DateTime? ValidUntilDateTime { get; set; }
        public double? Percentage { get; set; }
        public virtual List<DiscountProductModel>? Products { get; set; }
    }
}
