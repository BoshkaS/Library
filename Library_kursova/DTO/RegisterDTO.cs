using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library_kursova.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Email { get; set; }

        public string? UserImage { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
