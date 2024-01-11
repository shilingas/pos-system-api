using pos_system.Order;
using pos_system.Roles;
using System.Diagnostics.CodeAnalysis;

namespace pos_system.Workers
{
    public class WorkerModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public ICollection<WorkerRole> WorkerRoles { get; set; } = new List<WorkerRole>();
    }
}
