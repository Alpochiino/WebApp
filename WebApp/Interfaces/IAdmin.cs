using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IAdmin
    {
        Admin GetAdminById(int Id);
        IEnumerable<Admin> GetAdminByUsername(string username);
        IEnumerable<Admin> GetAdminByEmail(string email);
        IEnumerable<Admin> GetAllAdmins();
        bool IsEmailTaken(string email);
        void AddAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(int adminId);
        void SaveChanges();
    }
}
