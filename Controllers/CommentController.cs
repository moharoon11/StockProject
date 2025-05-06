using api.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; 
using api.Data;
using api.Dtos;
using api.Mappers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Repository;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        // Get all comments for a stock


     

        [AllowAnonymous]
        [HttpGet("{stockId}")]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsByStockIdAsync([FromRoute] int stockId)
        {
            var comments = await _commentRepo.GetCommentsByStockIdAsync(stockId);

            if (comments == null || comments.Count == 0)
            {
                return NotFound($"No comments found for stock with id {stockId}.");
            }

            var commentDtos = comments.Select(comment => comment.ToCommentDto()).ToList();
            return Ok(commentDtos);
        }

        // Get a single comment by its ID
        [AllowAnonymous]
        [HttpGet("comment/{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentByIdAsync([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound($"Comment with id {id} not found.");
            }

            var dto = comment.ToCommentDto();
            return Ok(dto);
        }

        // Create a new comment
        [HttpPost("create")]
        public async Task<ActionResult<CommentDto>> CreateCommentAsync([FromBody] CreateCommentRequest createCommentRequest)
        {
            if (createCommentRequest == null)
            {
                return BadRequest("Invalid comment data.");
            }

            var comment = createCommentRequest.ToComment();
            var createdComment = await _commentRepo.CreateCommentAsync(comment);

            return Ok(createdComment.ToCommentDto());
        }

        // Update an existing comment
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateCommentAsync([FromRoute] int id, [FromBody] UpdateCommentRequest updateCommentRequest)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound($"Comment with id {id} not found.");
            }

            if (updateCommentRequest == null)
            {
                return BadRequest("Invalid comment data.");
            }

            comment.Title = updateCommentRequest.Title;
            comment.Content = updateCommentRequest.Content;
            comment.CreatedOn = DateTime.Now; // Or keep the original date as needed
              
              
            await _commentRepo.UpdateCommentAsync(comment);
            return NoContent();
        }

        // Delete a comment
        // [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCommentAsync([FromRoute] int id)
        {
            var existingComment = await _commentRepo.GetByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound($"Comment with id {id} not found.");
            }

            await _commentRepo.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
