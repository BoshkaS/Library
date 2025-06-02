using System.ComponentModel.DataAnnotations;

namespace Library.DTO
{
    public class UserDTO
    {
        [Required]
        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public bool? IsMember { get; set; }

        public double Rating { get; set; }

        [Required]
        public string Email { get; set; }

        public string UserImage { get; set; }

        public string Token { get; set; }
    }
}
