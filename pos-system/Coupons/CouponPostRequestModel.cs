﻿namespace pos_system.Coupons
{
    public class CouponPostRequestModel
    {
        public decimal? Discount { get; set; }
        public string? ValidUntilDateTime { get; set; }
        public string? Status { get; set; }
    }
}
