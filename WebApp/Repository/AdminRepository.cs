using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Interfaces;

namespace WebApp.Repository
{
    public class AdminRepository : IAdmin
    {
        private readonly ApplicationDbContext context;

        public AdminRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Admin GetAdminById(int Id)
        {
            return context.Admins.FirstOrDefault(u => u.Id == Id);
        }

        public IEnumerable<Admin> GetAdminByUsername(string username)
        {
            return context.Admins.Where(u => u.Username == username).ToList();
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            return context.Admins.OrderBy(x => x.Username);
        }

        public IEnumerable<Admin> GetAdminByEmail(string email)
        {
            return context.Admins.Where(u => u.Email == email).ToList();
        }

        public bool IsEmailTaken(string email)
        {
            return context.Admins.Any(u => u.Email == email);
        }

        public void AddAdmin(Admin admin)
        {
            context.Admins.Add(admin);
        }

        public void UpdateAdmin(Admin admin)
        {
            context.Admins.Update(admin);
            context.SaveChanges();
        }

        public void DeleteAdmin(int adminId)
        {
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
