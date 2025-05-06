using api.Models;

namespace api.Dtos
{
    public class UpdateCommentRequest
    {
         public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}