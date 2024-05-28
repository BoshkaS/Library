using Library_kursova.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library_kursova.DTO
{
    public class BookDTORequest
    {
        [Required]
        public BookRequest Book { get; set; }

        [Required]
        public List<string> AuthorNames { get; set; }

        [Required]
        public string Language { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string PublishingHouse { get; set; }
    }
}
