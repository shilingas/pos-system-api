using System.Diagnostics.CodeAnalysis;

namespace pos_system.Order
{
    public class OrderRequestModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? CustomerId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public OrderStatus? Status { get; set; }
    }
}
