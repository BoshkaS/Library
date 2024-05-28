using System.Text.Json.Serialization;
using Library_kursova.Entities;

namespace Library_kursova.DTO
{
    public class BookRequest
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string BookImage { get; set; }

        public int NumberOfBorrows { get; set; }

        public int NumberOfComments { get; set; }

        public int NumberOfLikes { get; set; }

        public int YearOfPublication { get; set; }

        public int LanguageId { get; set; }

        [JsonIgnore]
        public Language Language { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        public int PublishingHouseId { get; set; }

        [JsonIgnore]
        public PublishingHouse PublishingHouse { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
    }
}
