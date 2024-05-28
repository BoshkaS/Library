using System.Text.Json.Serialization;

namespace Library_kursova.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}
