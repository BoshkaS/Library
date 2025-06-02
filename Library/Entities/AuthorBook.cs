using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class AuthorBook
    {
        public int AuthorBookId { get; set; }

        public int AuthorId { get; set; }

        [JsonIgnore]
        public Author Author { get; set; }

        public int BookId { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }
    }
}
