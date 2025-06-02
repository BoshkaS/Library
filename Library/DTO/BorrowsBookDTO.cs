namespace Library.DTO
{
    using System.ComponentModel.DataAnnotations;

    namespace Library_kursova.DTO
    {
        public class BorrowsBookDTO
        {
            [Required]
            public int UserId { get; set; } // Identifies the user by email

            [Required]
            public int BookId { get; set; } // Identifies the book being borrowed
        }
    }

}
