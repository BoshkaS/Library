namespace Library.DTO
{
    public class BorrowRequestDTO
    {
        public int RequestId { get; set; }
        public string Decision { get; set; } = string.Empty; // "approve" or "reject"
    }
}
