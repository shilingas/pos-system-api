using System.Diagnostics.CodeAnalysis;

namespace pos_system.Models
{
    public class ServiceModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public float? Duration { get; set; }
        public float? Price { get; set; }
    }
}
