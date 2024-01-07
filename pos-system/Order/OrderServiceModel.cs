using System.Diagnostics.CodeAnalysis;

namespace pos_system.Order
{
    public class OrderServiceModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public float? Duration { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string? OrderId {  get; set; }

    }
}
