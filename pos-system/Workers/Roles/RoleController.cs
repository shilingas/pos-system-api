using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace pos_system.Workers.Roles
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<RoleModel[]> GetRoles()
        {
            RoleModel[] roles = await _roleService.GetRoles();

            return roles;
        }

        [HttpPost]
        public async Task<ActionResult<RoleModel>> AddRole(string name)
        {
            RoleModel roleModel = await _roleService.AddRole(name);

            return Ok(roleModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> GetRoleById(string id)
        {
            RoleModel? role = await _roleService.GetRoleById(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);

        }

        [HttpPut("{id}/{roleName}")]
        public async Task<ActionResult<RoleModel>> UpdateRole(string id, string roleName)
        {
            RoleModel? role = await _roleService.UpdateRole(id, roleName);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            bool role = await _roleService.DeleteRole(id);

            if (role == false)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }

        }

    }
}
