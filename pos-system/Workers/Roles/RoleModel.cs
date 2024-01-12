using pos_system.Workers;
using System.Diagnostics.CodeAnalysis;

namespace pos_system.Workers.Roles
{
    public class RoleModel
    {
        [DisallowNull]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<WorkerRole> WorkerRoles { get; set; } = new List<WorkerRole>();
    }
}
