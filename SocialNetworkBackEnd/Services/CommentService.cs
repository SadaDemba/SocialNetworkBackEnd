using Microsoft.EntityFrameworkCore;
using SocialNetworkBackEnd.Data;
using SocialNetworkBackEnd.Models;
using SocialNetworkBackEnd.Interfaces;
using Microsoft.Data.SqlClient;
#nullable disable
namespace SocialNetworkBackEnd.Services
{ 
    public class CommentService : ICommentaire
    {
        private readonly SNDataContext _context;
        public CommentService(SNDataContext context)
        {
            _context=context;
        }

        public async Task<Comment> DeleteComment(int idU, int id)
        {
            if (!_context.Users.Any(u => u.UserId == idU))
                throw new BadHttpRequestException("Bad user id!!!");
            var comment = _context.Comments.Find(id);
            if (comment is null)
                return null;
            if(comment.UserId!=idU)
                throw new BadHttpRequestException("You can't remove other's comment!!!");
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> GetComment(int id)
        {
            if (!_context.Comments.Any(u => u.CommentId == id))
                return null;
            return await _context.Comments.AsNoTracking().FirstOrDefaultAsync(u => u.CommentId == id);
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _context.Comments.
                Include(c=>c.Publication).
                OrderBy(c=>c.PublicationId).
                OrderBy(c=>c.UserId).
                AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPublication(int id)
        {
            if (!_context.Publications.Any(p => p.PublicationId == id))
                return null;
            return await _context.Comments.
                Where(c => c.PublicationId == id).
                AsNoTracking().
                ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUser(int id)
        {
            if (!_context.Users.Any(u => u.UserId == id))
                return null;
            return await _context.Comments.
                Where(c => c.UserId == id).
                AsNoTracking().
                ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserAndPublication(int idU, int idP)
        {
            if (!_context.Users.Any(u => u.UserId == idU) || !_context.Publications.Any(p => p.PublicationId == idP))
                return null;
            return await _context.Comments.
                Where(c => c.UserId == idU).
                Where(c=>c.PublicationId==idP).
                AsNoTracking().
                ToListAsync();
        }

        public async Task<Comment> PostComment(Comment comment)
        {
            if (!_context.Publications.Any(u => u.PublicationId == comment.PublicationId))
                throw new BadHttpRequestException("Unknown post!!!");
            if (!_context.Users.Any(u => u.UserId == comment.UserId))
                throw new BadHttpRequestException("Unknown User!!!");
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> PutComment(int idU, int id, Comment comment)
        {
            Comment c =  _context.Comments.AsNoTracking().FirstOrDefault(c => c.CommentId == id);
            if (c is null)
                return null;
           
            if (c.UserId != idU || comment.CommentId != id)
                throw new BadHttpRequestException("Action not authorized");
            _context.Comments.Update(comment);
            try
            {
                await _context.SaveChangesAsync();
            }
             catch (DbUpdateConcurrencyException)
            {

            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sql)
                    HandleException(sql);
            }
            return comment;
        }
        private static void HandleException(SqlException sql)
        {
            if (sql.Message.Contains("IX_User_Email"))
                throw new BadHttpRequestException("Email déja existant!!!");
        }
    }
}
