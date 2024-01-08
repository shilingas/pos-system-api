using System.Diagnostics.CodeAnalysis;

namespace pos_system.Admins
{
    public class AdminModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
    }
}
