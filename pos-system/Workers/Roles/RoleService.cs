using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;

namespace pos_system.Workers.Roles
{
    public class RoleService : IRoleService
    {
        private readonly PosContext _context;
        public RoleService(PosContext _context)
        {
            this._context = _context;
        }

        public async Task<RoleModel[]> GetRoles()
        {
            RoleModel[] roles = await _context.Roles.ToArrayAsync();

            return roles;
        }

        public async Task<RoleModel> AddRole(string name)
        {
            RoleModel roleModel = new RoleModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
            };

            _context.Roles.Add(roleModel);
            await _context.SaveChangesAsync();

            return roleModel;
        }

        public async Task<RoleModel> GetRoleById(string id)
        {
            RoleModel? role = await _context.Roles.FindAsync(id);

            return role;

        }

        public async Task<RoleModel> UpdateRole(string id, string roleName)
        {
            RoleModel? role = await _context.Roles.FindAsync(id);

            if (roleName != null)
            {
                role.Name = roleName;
            }

            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteRole(string id)
        {
            RoleModel? role = await _context.Roles.FindAsync(id);
            if (role == null) { return false; }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
