using Library.Entities;

namespace Library.DTO
{
    public class CommentRequestDTO
    {
        public string Text { get; set; }

        public int BookId { get; set; }
    }
}
