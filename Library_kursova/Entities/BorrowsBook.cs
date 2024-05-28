using System.Text.Json.Serialization;

namespace Library_kursova.Entities
{
    public class BorrowsBook
    {
        public int BorrowsBookId { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public int BookId { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }

        public DateOnly DateOfBorrowing { get; set; }

        public DateOnly DateOfBorrowingExpiration { get; set; }
    }
}
