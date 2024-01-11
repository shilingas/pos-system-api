using System.Diagnostics.CodeAnalysis;

namespace pos_system.Order
{
    public enum OrderStatus
    {
        Created,
        InProgress,
        Completed,
    }
    public class OrderModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? CustomerId { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public OrderStatus? Status { get; set; }
        public decimal? Tip { get; set; }

    }
}
