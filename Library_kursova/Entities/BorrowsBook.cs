using System.Text.Json.Serialization;

namespace Library_kursova.Entities
{
    public class BorrowsBook
    {
        public int BorrowsBookId { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public int BookCopyId { get; set; }

        [JsonIgnore]
        public BookCopy BookCopy { get; set; }

        public DateOnly BorrowDate { get; set; }

        public DateOnly ReturnDate { get; set; }
    }
}
