using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IUser
    {
        Task <User> GetUserByIdAsync(int Id);
        Task <IEnumerable<User>> GetUserByUsernameAsync(string username);
        Task <IEnumerable<User>> GetUserByEmailAsync(string email);
        Task <IEnumerable<User>> GetAllUserAsync();
        Task <bool> IsPhoneNumberTakenAsync(string phoneNumber);
        Task <bool> IsEmailTakenAsync(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task SaveChangesAsync();
    }
}
