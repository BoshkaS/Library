namespace Library.DTO
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }

        public int BookCopyId { get; set; }

        public string BookTitle { get; set; }

        public string BookImage { get; set; }

        public int BookId { get; set; }

        public DateTime ReservedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool IsActive { get; set; }
    }
}
