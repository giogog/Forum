using Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CommentController(IServiceManager _serviceManager):ApiController(_serviceManager)
{
    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody]CreateCommentDto commentDto)
    {
        var user = await _serviceManager.UserService.GetUserWithClaim(User);
        await _serviceManager.CommentService.CreateComment(user.Id,commentDto);

        return Ok("Comment Added Succesfully");

    }
    [Authorize(Roles = "User")]
    [HttpPut("update-comment/{commentId}")]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDto commentDto,int commentId)
    {
        await _serviceManager.CommentService.UpdateComment(commentId, commentDto);

        return Ok("Comment updated Succesfully");

    }
    [Authorize(Roles = "User")]
    [HttpDelete("delete-comment/{commentId}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        await _serviceManager.CommentService.DeleteComment(commentId);

        return Ok("Comment deleted Succesfully");

    }
}
