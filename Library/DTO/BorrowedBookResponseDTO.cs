namespace Library.DTO
{
    public class BorrowedBookResponseDTO
    {
        public int BorrowsBookId { get; set; }

        public int BookId { get; set; }

        public string Title { get; set; }

        public string BookImage { get; set; }

        public DateOnly BorrowDate { get; set; }

        public DateOnly ReturnDate { get; set; }

        public DateOnly? ActualReturnDate { get; set; }

        public bool IsReturned { get; set; }
    }
}
