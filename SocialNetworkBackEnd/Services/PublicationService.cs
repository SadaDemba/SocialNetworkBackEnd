using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SocialNetworkBackEnd.Data;
using SocialNetworkBackEnd.Interfaces;
using SocialNetworkBackEnd.Models;
#nullable disable
namespace SocialNetworkBackEnd.Services
{
    public class PublicationService : IPublication
    {
        private readonly SNDataContext _context;
        public PublicationService(SNDataContext context)
        {
            _context = context;
        }
        public async Task<Publication> DeletePublication(int idU, int id)
        {
            if (!_context.Users.Any(u => u.UserId == idU))
                throw new BadHttpRequestException("Bad user id!!!");
                var pub = _context.Publications.Find(id);
            if (pub is null)
                return null;
            if(pub.UserId!=idU)
                throw new BadHttpRequestException("You can't remove other's post!!!");
            _context.Publications.Remove(pub);
            await _context.SaveChangesAsync();
            return pub;
        }

        public async Task<Publication> GetPublication(int id)
        {
            var publication = await _context.Publications.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PublicationId == id);
            if (publication is null)
                return null;
            return publication;
        }

        public async Task<IEnumerable<Publication>> GetPublications()
        {
            return  await _context.Publications.AsNoTracking()
               .Include(p=>p.User).
               OrderBy(p=>p.PublicationId).
               ToListAsync();
        }

        public async Task<IEnumerable<Publication>> GetPublicationsByUser(int idU)
        {
            if (!_context.Users.Any(u => u.UserId == idU))
                return null;
            return await _context.Publications.AsNoTracking().
                Where(p => p.UserId == idU).
                ToListAsync();
        }

        public async Task<Publication> PostPublication(Publication publication)
        {
            if (!_context.Users.Any(u => u.UserId == publication.UserId))
                return null;
            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();
            return publication;
        }
        
        public async Task<Publication> PutPublication(int idU, int id, Publication publication)
        {
            if (publication.PublicationId != id)
                return null;
            if (!_context.Publications.Any(p => p.PublicationId == id))
                throw new BadHttpRequestException("missing Publication!!!");
            if(!_context.Users.Any(u => u.UserId == publication.UserId))
                throw new BadHttpRequestException("missing User!!!");
            if (!_context.Users.Any(u => u.UserId == idU))
                throw new BadHttpRequestException("You're not a User!!!");

            if (idU != publication.UserId)
                throw new BadHttpRequestException("You can't modify a post on which you're not the author!!!");
            _context.Publications.Update(publication);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ) {
                
            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sql)
                    HandleException(sql);
            }
            return publication;
           
        }


        private static void HandleException(SqlException sql)
        {
            if (sql.Message.Contains("IX_User_Email"))
                throw new BadHttpRequestException("Email déja existant!!!");
        }
    }
}
