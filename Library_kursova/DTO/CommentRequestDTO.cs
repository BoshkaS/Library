using Library_kursova.Entities;

namespace Library_kursova.DTO
{
    public class CommentRequestDTO
    {
         public string Email { get; set; }

        public string Text { get; set; }

        public int BookId { get; set; }
    }
}
