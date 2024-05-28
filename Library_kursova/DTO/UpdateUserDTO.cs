using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library_kursova.DTO
{
    public class UpdateUserDTO
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Email { get; set; }

        [JsonIgnore]
        public string UserImage { get; set; }
    }
}

        
