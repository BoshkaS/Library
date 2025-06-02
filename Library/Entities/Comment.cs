using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Text { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public int BookId { get; set; }

        [JsonIgnore]
        public Book? Book { get; set; }
    }
}
