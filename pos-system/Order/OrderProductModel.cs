using pos_system.Products;
using System.Diagnostics.CodeAnalysis;

namespace pos_system.Order
{
    public class OrderProductModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? ProductId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Category { get; set; }
        public int? Quantity { get; set; }

        public string? OrderId { get; set; }
        public virtual OrderModel? Order { get; set; }

    }
}
