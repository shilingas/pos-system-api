﻿using pos_system.Order;
using System.Diagnostics.CodeAnalysis;

namespace pos_system.Models
{
    public class ProductModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Category { get; set; }
    }
}
