using Microsoft.EntityFrameworkCore;
using SocialNetworkBackEnd.Data;
using SocialNetworkBackEnd.Interfaces;
using SocialNetworkBackEnd.Models;
#nullable disable
namespace SocialNetworkBackEnd.Services
{
    public class LikeService : ILike
    {
        private readonly SNDataContext _context;
        public LikeService(SNDataContext context)=>_context=context;
        public async Task<IEnumerable<Like>> GetLikes()
        {
            return await _context.Likes.AsNoTracking().ToListAsync();
        }

        public async Task<Like> GetLike(int id)
        {
            if (!_context.Likes.Any(u => u.LikeId == id))
                return null;
            return await _context.Likes.Include(l => l.Publication).FirstOrDefaultAsync(l => l.LikeId == id) ;
        }
        public async Task<IEnumerable<Like>> GetLikesByPublication(int id)
        {
            if (!_context.Publications.Any(p => p.PublicationId == id))
                return null;
            return await _context.Likes.AsNoTracking().Where(l=>l.PublicationId==id).Include(l => l.Publication).ToListAsync();
        }

        public async Task<IEnumerable<Like>> GetLikesByUser(int id)
        {
            if (!_context.Users.Any(u => u.UserId == id))
                return null;
            return await _context.Likes.Include(l=>l.Publication).
                AsNoTracking().Where(l => l.Auteur == id).ToListAsync();
        }

        public async Task<Like> Like(Like like)
        {
            if (!_context.Publications.Any(u => u.PublicationId == like.PublicationId))
                throw new BadHttpRequestException("Unknown post!!!");
            if (!_context.Users.Any(u => u.UserId == like.Auteur))
                throw new BadHttpRequestException("Unknown User!!!");
            if(_context.Likes.Any(l=> (l.Auteur == like.Auteur && l.LikeId == like.LikeId) ))
                throw new BadHttpRequestException("Post already liked!!!");
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
            return like;
        }

        public async Task<Like> UnLike(int idU, int id)
        {
            var like = _context.Likes.Find(id);
            if (like == null)
                return null;
            if (!_context.Users.Any(u => u.UserId == idU))
                throw new BadHttpRequestException("Unknown User!!!");
            if(idU!=like.Auteur)
                throw new BadHttpRequestException("Operation denied!!!");
             _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return like;
        }
    }
}
