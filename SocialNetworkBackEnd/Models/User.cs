using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialNetworkBackEnd.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Le champ PRENOM est obligatoire!!!")]
        public string Fname { get; set; }= String.Empty;

        [Required(ErrorMessage = "Le champ NOM est obligatoire!!!")]
        public string Lname { get; set; } = String.Empty;

        [Required(ErrorMessage = "Le champ EMAIL est obligatoire!!!")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Le champ MOT DE PASSE est obligatoire!!!")]
        public string Password { get; set; } = String.Empty;
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDay { get; set; }

        [JsonIgnore]
        public virtual List<Publication> Publications { get; set; }


        public User()
        {
            Publications= new List<Publication>();
        }

    }
}
