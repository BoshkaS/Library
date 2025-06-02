using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Library.Entities
{
    public class User : IdentityUser<int>
    {
        public string Nickname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


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

        public bool IsMember { get; set; }

        public double Rating { get; set; } = 10.0;

        [JsonIgnore]
        public ICollection<AppUserRole> UserRoles { get; set; }

        [JsonIgnore]
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        [JsonIgnore]
        public ICollection<UserRatingLog> RatingLogs { get; set; } = new List<UserRatingLog>();
    }
}
