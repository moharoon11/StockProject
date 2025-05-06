using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all comments by StockId
        public async Task<List<Comment>> GetCommentsByStockIdAsync(int stockId)
        {
            return await _context.Comments
                                 .Where(c => c.StockId == stockId)
                                 .Include(c => c.Stock) // You can include the Stock if needed
                                 .ToListAsync();
        }

        // Get a single comment by Id
        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments
                                 .Include(c => c.Stock) // Include stock data if needed
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Create a new comment
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        // Update an existing comment
        public async Task UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        // Delete a comment by Id
        public async Task DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
