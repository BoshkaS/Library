using System.ComponentModel.DataAnnotations;

namespace Library.DTO
{
    public class BookCheckDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public List<string> AuthorNames { get; set; } = new List<string>();

        [Required]
        public string PublishingHouse { get; set; }
    }
}
