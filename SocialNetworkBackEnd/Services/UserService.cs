using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SocialNetworkBackEnd.Data;
using SocialNetworkBackEnd.Interfaces;
using SocialNetworkBackEnd.Models;
#nullable disable
namespace SocialNetworkBackEnd.Services
{
    public class UserService : IUser
    {
        private readonly SNDataContext _context;
        public UserService(SNDataContext context)
        {
            _context = context;
        }
        public async Task<User> DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user is null)
                return null;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return  user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user =  await _context.Users.AsNoTracking().FirstOrDefaultAsync(u=>u.Email==email);
            if (user is null)
                return null;
            return user;
     
        }

        public async Task<User> GetUserById(int id)
        {
            User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);
            if (user is null)
                return null;
            return user;
        }

        public async Task <IEnumerable<User>> GetUsers()
        {
            return  await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> PostUser(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }catch(DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sql)
                    HandleException(sql);
            }
            return user;
        }

      

        public async Task<User> PutUser(int id, User user)
        {
            if (id != user.UserId) 
                return null;
            if (!_context.Users.Any(u=>u.UserId==id))
                throw new BadHttpRequestException("User doesn't exist!!!");
            _context.Users.Update(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ) {}
            
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sql)
                    HandleException(sql);
            }
            return user;
            
        }

        private static void HandleException(SqlException sql)
        {
            if (sql.Message.Contains("IX_User_Email"))
                throw new BadHttpRequestException("Email déja existant!!!");
        }
    }
}
