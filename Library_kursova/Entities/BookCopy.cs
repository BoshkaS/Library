using System.Text.Json.Serialization;

namespace Library_kursova.Entities
{
    public class BookCopy
    {
        public int BookCopyId { get; set; }

        public int BookId { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }

        public BookStatus Status { get; set; }

        [JsonIgnore]
        public ICollection<BorrowsBook> BookBorrows { get; set; } = new List<BorrowsBook>();
    }
}
