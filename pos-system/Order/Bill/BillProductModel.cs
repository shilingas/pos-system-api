﻿namespace pos_system.Order.Bill
{
    public class BillProductModel
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? Vat { get; set; }
        public int? Quantity { get; set; }
    }
}
