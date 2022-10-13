using SocialNetworkBackEnd.Models;

namespace SocialNetworkBackEnd.Interfaces
{
    public interface ILike
    {
        Task<IEnumerable<Like>> GetLikes();
        Task<IEnumerable<Like>> GetLikesByPublication(int id);
        Task<IEnumerable<Like>> GetLikesByUser(int id);
        Task<Like> GetLike(int id);
        Task<Like> Like(Like like);
        Task<Like> UnLike(int idU, int id);
    }
}
