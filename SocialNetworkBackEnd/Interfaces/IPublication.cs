using SocialNetworkBackEnd.Models;

namespace SocialNetworkBackEnd.Interfaces
{
    public interface IPublication
    {
        Task<IEnumerable<Publication>> GetPublications();
        Task<IEnumerable<Publication>> GetPublicationsByUser(int id);
        Task<Publication> GetPublication( int id);
        Task<Publication> PostPublication(Publication publication);
        Task<Publication> PutPublication(int idU, int id, Publication publication);
        Task<Publication> DeletePublication(int idU, int id);
    }
}
