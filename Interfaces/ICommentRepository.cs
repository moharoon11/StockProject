using api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByStockIdAsync(int stockId);
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
    }
}
