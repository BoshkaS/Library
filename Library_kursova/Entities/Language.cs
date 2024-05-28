using System.Text.Json.Serialization;

namespace Library_kursova.Entities
{
    public class Language
    {
        public int LanguageId { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}
