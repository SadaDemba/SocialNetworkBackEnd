using SocialNetworkBackEnd.Models;

namespace SocialNetworkBackEnd.Interfaces
{
    public interface ICommentaire
    {
        Task <IEnumerable<Comment>> GetComments();
        Task<IEnumerable<Comment>> GetCommentsByUser(int id);
        Task<IEnumerable<Comment>> GetCommentsByPublication(int id);
        Task<IEnumerable<Comment>> GetCommentsByUserAndPublication(int idU, int idP);
        Task<Comment> GetComment(int id);
        Task<Comment> DeleteComment(int idU, int id);
        Task<Comment> PostComment(Comment comment);
        Task<Comment> PutComment(int idU, int id, Comment comment);
    }
}
