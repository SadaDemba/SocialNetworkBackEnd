using SocialNetworkBackEnd.Models;

namespace SocialNetworkBackEnd.Interfaces;

public interface IUser
{
    Task<IEnumerable<User>> GetUsers();
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByEmail(string email);
    Task<User> PostUser(User user);
    Task<User?> PutUser(int id, User user);
    Task<User?> DeleteUser(int id);
}
