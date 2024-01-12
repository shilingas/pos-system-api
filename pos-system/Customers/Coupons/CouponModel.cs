using pos_system.ProductService.Reservation;
using System.Diagnostics.CodeAnalysis;

namespace pos_system.Customers.Coupons
{
    public class CouponModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public decimal? Discount { get; set; }
        public string? ValidUntilDateTime { get; set; }
        public string? Status { get; set; }
    }
}
