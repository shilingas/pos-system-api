using pos_system.Customers;

namespace pos_system.Admins
{
    public interface IAdminService
    {
        Task<AdminModel[]> GetAllAdmins();
        Task<AdminModel?> CreateAdmin(AdminPostRequestModel adminModel);
        Task<AdminModel?> GetAdmin(string id);
        Task<AdminModel?> UpdateAdmin(string adminId, AdminPostRequestModel adminModel);
        Task<bool> DeleteAdmin(string id);
    }
}
