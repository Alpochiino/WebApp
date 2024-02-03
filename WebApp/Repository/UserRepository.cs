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

        public User GetUserById(int Id)
        {
            return context.Users.FirstOrDefault(u => u.Id == Id);
        }

        public IEnumerable<User> GetUserByUsername(string username)
        {
            return context.Users.Where(u => u.Username == username).ToList();
        }

        public IEnumerable<User> GetAllUser()
        {
            return context.Users.OrderBy(x => x.LastName);
        }

        public IEnumerable<User> GetUserByEmail(string email)
        {
            return context.Users.Where(u => u.Email == email).ToList();
        }

        public bool IsPhoneNumberTaken(string phoneNumber)
        {
            return context.Users.Any(u => u.PhoneNumber == phoneNumber);
        }

        public bool IsEmailTaken(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public void DeleteUser(int UserId)
        {
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
