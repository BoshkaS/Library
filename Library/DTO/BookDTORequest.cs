using Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.DTO
{
    public class BookDTORequest
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
    }
}
