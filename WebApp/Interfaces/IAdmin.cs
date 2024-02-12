using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IAdmin
    {
        Admin GetAdminById(int Id);
        Task <IEnumerable<Admin>> GetAdminByUsernameAsync(string username);
        Task <IEnumerable<Admin>> GetAdminByEmailAsync(string email);
        Task <IEnumerable<Admin>> GetAllAdminsAsync();
        Task <bool> IsEmailTakenAsync(string email);
        Task AddAdminAsync(Admin admin);
        Task UpdateAdminAsync(Admin admin);
        Task DeleteAdminAsync(int adminId);
        Task SaveChangesAsync();
    }
}
