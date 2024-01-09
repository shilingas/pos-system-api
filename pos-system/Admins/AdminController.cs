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
        public AdminController(PosContext _context)
        {
            this._context = _context;
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<AdminModel>> CreateAdmin([FromBody] AdminPostRequestModel adminModel)
        {
            AdminModel admin = new AdminModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = adminModel.Name,
                Email = adminModel.Email,
                Phone = adminModel.Phone,
                Password = adminModel.Password,
                Username = adminModel.Username,
            };
            _context.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminModel>?> GetAdmin(string id)
        {
            AdminModel? admin = new AdminModel();
            admin = await _context.Admins.FindAsync(id);
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
            AdminModel[] admins = new AdminModel[0];
            if (_context != null)
            {
                admins = await _context.Admins.ToArrayAsync();
            }

            return admins;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAdmin(string id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return false;
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return true;
        }

        [HttpPut("{adminId}")]
        public async Task<AdminModel?> UpdateAdmin(string adminId, AdminPostRequestModel adminModel)
        {
            AdminModel? updated = new AdminModel();
            if (_context != null)
            {
                updated = await _context.Admins.SingleOrDefaultAsync(admin => admin.Id == adminId);
                if (updated == null)
                {
                    return null;
                }
                if (adminModel.Name != null)
                {
                    updated.Name = adminModel.Name;
                }
                if (adminModel.Email != null)
                {
                    updated.Email = adminModel.Email;
                }
                if (adminModel.Phone != null)
                {
                    updated.Phone = adminModel.Phone;
                }
                if (adminModel.Password != null)
                {
                    updated.Password = adminModel.Password;
                }
                if (adminModel.Username != null)
                {
                    updated.Username = adminModel.Username;
                }
                await _context.SaveChangesAsync();
                return updated;
            }
            return null;

        }
    }
}
