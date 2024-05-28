using System.ComponentModel.DataAnnotations;
using Library_kursova.Entities;

namespace Library_kursova.DTO
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
    }
}
