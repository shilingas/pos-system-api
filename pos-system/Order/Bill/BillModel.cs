using System.Diagnostics.CodeAnalysis;

namespace pos_system.Order.Bill
{
    public class BillModel
    {
        public string? CustomerId { get; set; }
        public string? Customer { get; set; }
        public DateTime? GeneratedDateTime { get; set; }
        public List<BillProductModel>? Products { get; set; }
        public List<BillServiceModel>? Services { get; set; }
        public decimal? Total { get; set; }
        public decimal? Tip { get; set; }
    }
}
