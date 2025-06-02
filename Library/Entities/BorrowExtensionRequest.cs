using System.Text.Json.Serialization;

namespace Library.Entities
{
    public class BorrowExtensionRequest
    {
        public int BorrowExtensionRequestId { get; set; }

        public int BorrowsBookId { get; set; }

        [JsonIgnore]
        public BorrowsBook BorrowsBook { get; set; }

        public bool? Approved { get; set; }  // NULL = Pending, True = Approved, False = Rejected

        [JsonIgnore]
        public DateTime RequestedAt { get; set; }
    }
}
