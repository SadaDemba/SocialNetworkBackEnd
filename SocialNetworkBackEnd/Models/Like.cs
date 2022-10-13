using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBackEnd.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int PublicationId { get; set; }
        public int Auteur { get; set; }
        [JsonIgnore]
        public virtual Publication? Publication { get; set; }
        // public int CommentId { get; set; }
      
    }
}
