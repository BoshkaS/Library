using System.ComponentModel.DataAnnotations;
using Library.Entities;

namespace Library.DTO
{
    public class BookDTO
    {
        [Required]
        public Book Book { get; set; }

        [Required]
        public List<string> AuthorNames { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string PublishingHouse { get; set; }

        public List<CommentResponseDTO> Comments { get; set; }

        public int Copies { get; set; }

        public string AvailabilityStatus { get; set; }

        public DateTime? NearestReservationExpiry { get; set; }
    }
}
