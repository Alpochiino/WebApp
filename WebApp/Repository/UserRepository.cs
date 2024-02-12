using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Repository
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task <User> GetUserByIdAsync(int Id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == Id);
        }

        public async Task <IEnumerable<User>> GetUserByUsernameAsync(string username)
        {
            return await context.Users.Where(u => u.Username == username).ToListAsync();
        }

        public async Task <IEnumerable<User>> GetAllUserAsync()
        {
            return await context.Users.OrderBy(x => x.LastName).ToListAsync();
        }

        public async Task <IEnumerable<User>> GetUserByEmailAsync(string email)
        {
            return await context.Users.Where(u => u.Email == email).ToListAsync();
        }

        public async Task <bool> IsPhoneNumberTakenAsync(string phoneNumber)
        {
            return await context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task <bool> IsEmailTakenAsync(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int UserId)
        {
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
