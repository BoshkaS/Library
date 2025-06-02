using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class LikedBook
    {
        public int LikedBookId { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public int BookId { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }
    }
}
