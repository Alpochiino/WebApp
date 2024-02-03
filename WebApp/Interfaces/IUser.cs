using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IUser
    {
        User GetUserById(int Id);
        IEnumerable<User> GetUserByUsername(string username);
        IEnumerable<User> GetUserByEmail(string email);
        IEnumerable<User> GetAllUser();
        bool IsPhoneNumberTaken(string phoneNumber);
        bool IsEmailTaken(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        void SaveChanges();
    }
}
