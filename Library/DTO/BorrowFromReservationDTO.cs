using System.ComponentModel.DataAnnotations;

namespace Library.DTO
{
    public class BorrowFromReservationDTO
    {
        [Required]
        public int UserId { get; set; } // Identifies the user by email

        [Required]
        public int BookCopyId { get; set; } // Identifies the book being borrowed
    }
}
