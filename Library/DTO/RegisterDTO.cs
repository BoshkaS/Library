using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library.DTO
{
    public class RegisterDTO
    {
        public int? Id { get; set; }
        [Required]
        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public bool? IsMember { get; set; }

        public string? UserImage { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
