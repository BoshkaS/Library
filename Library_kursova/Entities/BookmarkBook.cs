using System.Text.Json.Serialization;

namespace Library_kursova.Entities
{
    public class BookmarkBook
    {
        public int BookmarkBookId { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public int BookId { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }
    }
}
