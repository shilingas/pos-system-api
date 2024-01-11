using Microsoft.AspNetCore.Mvc;
using pos_system.Contexts;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace pos_system.Roles
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly PosContext _context;
        public RoleController(PosContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public async Task<RoleModel[]> GetRoles()
        {
            RoleModel[] roles = await _context.Roles.ToArrayAsync();

            return roles;
        }

        [HttpPost]
        public async Task<ActionResult<RoleModel>> AddRole(string name)
        {
            RoleModel roleModel = new RoleModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
            };

            _context.Roles.Add(roleModel);
            await _context.SaveChangesAsync();

            return Ok(roleModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> GetRoleById(string id)
        {
            RoleModel? role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;

        }

        [HttpPut("{id}/{roleName}")]
        public async Task<ActionResult<RoleModel>> UpdateRole(string id, string roleName)
        {
            RoleModel? role = await _context.Roles.FindAsync(id);

            if (role == null) { return NotFound(); }

            if (roleName != null)
            {
                role.Name = roleName;
            }

            await _context.SaveChangesAsync();
            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            RoleModel? role = await _context.Roles.FindAsync(id);
            if (role == null) { return NotFound(); }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
