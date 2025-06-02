using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string BookImage { get; set; }

        public int NumberOfBorrows { get; set; }

        public int NumberOfComments { get; set; }

        public int NumberOfLikes { get; set; }

        public int YearOfPublication { get; set; }

        public string ISBN { get; set; }

        public int LanguageId { get; set; }

        [JsonIgnore]
        public Language Language { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        [JsonIgnore]
        public virtual ICollection<AuthorBook> BookAuthors { get; set; }

        [JsonIgnore]
        public virtual ICollection<BookmarkBook> BookBookmarks { get; set; }

        [JsonIgnore]
        public virtual ICollection<LikedBook> BookLikes { get; set; }

        [JsonIgnore]
        public virtual ICollection<BookCopy> Copies { get; set; }

        public int PublishingHouseId { get; set; }

        [JsonIgnore]
        public PublishingHouse PublishingHouse { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

    }
}
