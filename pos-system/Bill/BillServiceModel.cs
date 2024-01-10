namespace pos_system.Bill
{
    public class BillServiceModel
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? Vat {  get; set; }
        public int? Quantity { get; set; }
    }
}
