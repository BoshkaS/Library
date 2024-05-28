using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Library_kursova.Entities
{
    public class User : IdentityUser<int>
    {
        public string Nickname { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment>? Comments { get; set; }

        [JsonIgnore]
        public virtual ICollection<BookmarkBook>? BookmarkBooks { get; set; }

        [JsonIgnore]
        public virtual ICollection<LikedBook>? LikedBooks { get; set; }

        [JsonIgnore]
        public virtual ICollection<BorrowsBook>? BorrowsBooks { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

        public string? UserImage { get; set; }

        [JsonIgnore]
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
