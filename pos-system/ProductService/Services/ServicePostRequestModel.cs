using System.Diagnostics.CodeAnalysis;

namespace pos_system.ProductService.Services
{
    public class ServicePostRequestModel
    {
        [DisallowNull]
        public string? Name { get; set; }
        public float? Duration { get; set; }
        public decimal? Price { get; set; }
    }
}
