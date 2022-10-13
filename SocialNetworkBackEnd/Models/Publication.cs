using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialNetworkBackEnd.Models
{
    public class Publication
    {
        [Key]
        public int PublicationId { get; set; }
        public int UserId { get; set; } 

        [Required(ErrorMessage ="Renseignez le contenu de votre publication!!!")]
        public string Contenu { get; set; }= string.Empty;

        
        public virtual User? User { get; set; }

        [JsonIgnore]
        public virtual List<Comment> Comments { get; set; }
        [JsonIgnore]
        public virtual List<Like> Likes { get; set; }
        public Publication()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
           
        }
    }
}
