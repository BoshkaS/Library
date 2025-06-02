using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string Pseudonym { get; set; }

        [JsonIgnore]
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
