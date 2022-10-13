using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBackEnd.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PublicationId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Renseignez le contenu de votre publication!!!")]
        public string Contenu { get; set; } = string.Empty;
        public virtual Publication? Publication { get; set; }
    }
}
