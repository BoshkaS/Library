using Newtonsoft.Json;

namespace Library.DTO
{
    public class SimilarBookDTO
    {
        [JsonProperty("book_id")]
        public int BookId { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }
    }
}
