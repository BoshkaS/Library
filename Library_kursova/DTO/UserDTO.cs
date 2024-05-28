using System.ComponentModel.DataAnnotations;

namespace Library_kursova.DTO
{
    public class UserDTO
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Email { get; set; }

        public string UserImage { get; set; }

        public string Token { get; set; }
    }
}
