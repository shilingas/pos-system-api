using System.Diagnostics.CodeAnalysis;

namespace pos_system.Customers
{
    public class CustomerModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
