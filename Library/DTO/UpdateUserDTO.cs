using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library.DTO
{
    public class UpdateUserDTO
    {
        [Required]
        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsMember { get; set; }

        [Required]
        public string Email { get; set; }

        [JsonIgnore]
        public string UserImage { get; set; }
    }
}

        
