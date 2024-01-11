using pos_system.Roles;

namespace pos_system.Workers
{
    public class WorkerDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Username { get; set; }
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}
