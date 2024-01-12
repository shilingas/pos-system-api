using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;
using pos_system.Customers;

namespace pos_system.Admins
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly PosContext _context;
        private readonly IAdminService _adminService;
        public AdminController(PosContext _context, IAdminService adminService)
        {
            this._context = _context;
            _adminService = adminService;
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<AdminModel>> CreateAdmin([FromBody] AdminPostRequestModel adminModel)
        {
            AdminModel? admin = await _adminService.CreateAdmin(adminModel);
            if (admin == null)
            {
                return BadRequest();
            }
            return Ok(admin);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminModel>?> GetAdmin(string id)
        {
            AdminModel? admin = await _adminService.GetAdmin(id);
            if (admin != null)
            {
                return admin;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public async Task<AdminModel[]> GetAllAdmins()
        {
            return await _adminService.GetAllAdmins();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdmin(string id)
        {
            bool isDeleted = await _adminService.DeleteAdmin(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{adminId}")]
        public async Task<ActionResult<AdminModel>> UpdateAdmin(string adminId, AdminPostRequestModel adminModel)
        {
            AdminModel? admin = await _adminService.UpdateAdmin(adminId, adminModel);
            if (admin != null)
            {
                return Ok(admin);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
