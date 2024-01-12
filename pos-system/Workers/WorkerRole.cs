using pos_system.Workers;
using pos_system.Workers.Roles;

namespace pos_system
{
    public class WorkerRole
    {
        public string? Id { get; set; }
        public string? WorkerId { get; set; }
        public WorkerModel? Worker { get; set; }

        public string? RoleId { get; set; }
        public RoleModel? Role { get; set; }
    }
}
