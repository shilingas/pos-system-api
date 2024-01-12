using System.Diagnostics.CodeAnalysis;

namespace pos_system.Order.Payments
{
    public class PaymentModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }

    }
}
