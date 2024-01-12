using Microsoft.EntityFrameworkCore;
using pos_system.Contexts;

namespace pos_system.Management.Admins
{
    public class AdminService : IAdminService
    {
        private readonly PosContext _context;
        public AdminService(PosContext context)
        {
            _context = context;
        }
        public async Task<AdminModel?> CreateAdmin(AdminPostRequestModel adminModel)
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

        public async Task<AdminModel?> GetAdmin(string id)
        {
            AdminModel? admin = new AdminModel();
            admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                return admin;
            }
            else
            {
                return null;

            }
        }

        public async Task<AdminModel[]> GetAllAdmins()
        {
            AdminModel[] admins = new AdminModel[0];
            if (_context != null)
            {
                admins = await _context.Admins.ToArrayAsync();
            }

            return admins;
        }

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
