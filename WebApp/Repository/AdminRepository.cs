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

        public async Task <IEnumerable<Admin>> GetAdminByUsernameAsync(string username)
        {
            return await context.Admins.Where(u => u.Username == username).ToListAsync();
        }

        public async Task <IEnumerable<Admin>> GetAllAdminsAsync()
        {
            return await context.Admins.OrderBy(x => x.Username).ToListAsync();
        }

        public async Task <IEnumerable<Admin>> GetAdminByEmailAsync(string email)
        {
            return await context.Admins.Where(u => u.Email == email).ToListAsync();
        }

        public async Task <bool> IsEmailTakenAsync(string email)
        {
            return await context.Admins.AnyAsync(u => u.Email == email);
        }

        public async Task AddAdminAsync(Admin admin)
        {
            await context.Admins.AddAsync(admin);
        }

        public async Task UpdateAdminAsync(Admin admin)
        {
            context.Admins.Update(admin);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAdminAsync(int adminId)
        {
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
