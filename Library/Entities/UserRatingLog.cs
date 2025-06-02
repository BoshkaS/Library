using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class UserRatingLog
    {
        public int UserRatingId { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public double ChangeAmount { get; set; }

        public string Reason { get; set; } = string.Empty;

        public double RatingAfterChange { get; set; }
    }
}
