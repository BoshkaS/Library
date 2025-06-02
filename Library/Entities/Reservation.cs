using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int BookCopyId { get; set; }

        [JsonIgnore]
        public BookCopy BookCopy { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;

        public DateTime ExpiresAt => ReservedAt.AddDays(3);

        public bool IsActive { get; set; }
    }
}
