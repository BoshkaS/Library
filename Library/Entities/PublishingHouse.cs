using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class PublishingHouse
    {
        public int PublishingHouseId { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}
