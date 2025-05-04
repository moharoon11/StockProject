using api.Models;

namespace api.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? stockId { get; set; }

        // navigation property
        public Stock? Stock { get; set; }



        public CommentDto commentToDTO(Comment commentModel)
        {
            CommentDto commentDto = new CommentDto();
            commentDto.Id = commentModel.Id;
            commentDto.Title = commentModel.Title;
            commentDto.Content = commentModel.Content;
            commentDto.CreatedOn = commentModel.CreatedOn;
            commentDto.stockId = commentModel.stockId;
            return commentDto;
        }
    }
}