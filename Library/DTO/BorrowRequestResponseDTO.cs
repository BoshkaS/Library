namespace Library.DTO
{
    public class BorrowRequestResponseDTO
    {
        public int RequestId { get; set; }
        public int BorrowsBookId { get; set; }
        public string UserEmail { get; set; } // Or Username
        public string BookTitle { get; set; }
        public int BookCopyId { get; set; }
        public DateOnly CurrentReturnDate { get; set; }
        public DateTime RequestDate { get; set; }
        public bool? Approved { get; set; } // NULL = pending, true = approved, false = rejected
    }
}
