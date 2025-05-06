using api.Models;

namespace api.Dtos {

    public class CreateCommentRequest
    {

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? StockId { get; set; }

        // navigation property
        public Stock? Stock { get; set; }
    }
}