using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class BookCopy
    {
        public int BookCopyId { get; set; }

        public int BookId { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }

        [JsonIgnore]
        public ICollection<BorrowsBook> BookBorrows { get; set; } = new List<BorrowsBook>();

        [JsonIgnore]
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
