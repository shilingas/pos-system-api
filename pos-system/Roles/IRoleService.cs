using Microsoft.AspNetCore.Mvc;

namespace pos_system.Roles
{
    public interface IRoleService
    {
        Task<RoleModel> AddRole(string name);
        Task<bool> DeleteRole(string id);
        Task<RoleModel> GetRoleById(string id);
        Task<RoleModel[]> GetRoles();
        Task<RoleModel> UpdateRole(string id, string roleName);
    }
}