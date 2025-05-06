using api.Models;
using api.Dtos;

namespace api.Mappers
{
    public static class CommentMapper
    {
        // Map Comment model to CommentDto
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
                Stock = comment.Stock // Navigation property to Stock
                // Use StockId t
            };
        }

        // Map CommentCreateDto to Comment model
        public static Comment ToComment(this CreateCommentRequest commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = commentDto.StockId,
                Stock = commentDto.Stock // Relates the comment to a stock
            };
        }

        
    }
}
