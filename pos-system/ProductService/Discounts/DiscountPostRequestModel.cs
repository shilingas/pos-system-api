﻿using System.Diagnostics.CodeAnalysis;

namespace pos_system.ProductService.Discounts
{
    public class DiscountPostRequestModel
    {
        public DateTime? ValidUntilDateTime { get; set; }
        public double? Percentage { get; set; }
    }
}
