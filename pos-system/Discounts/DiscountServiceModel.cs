using System.Diagnostics.CodeAnalysis;

namespace pos_system.Discounts
{
    public class DiscountServiceModel
    {
        [DisallowNull]
        public String? Id { get; set; }
        public String? ServiceId { get; set; }
        public String? DiscountId { get; set; }
        public String? Name { get; set; }
    }
}
