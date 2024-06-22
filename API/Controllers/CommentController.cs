using Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

public class CommentController(IServiceManager _serviceManager):ApiController(_serviceManager)
{
    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody]CreateCommentDto commentDto)
    {
        var user = await _serviceManager.UserService.GetUserWithClaim(User);
        await _serviceManager.CommentService.CreateComment(user.Id,commentDto);

        _response = new ApiResponse("Comment Added Succesfully", true, null, Convert.ToInt32(HttpStatusCode.Created));
        return StatusCode(_response.StatusCode, _response);

    }
    [Authorize(Roles = "User")]
    [HttpPut("update-comment/{commentId}")]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDto commentDto,int commentId)
    {
        var user = await _serviceManager.UserService.GetUserWithClaim(User);
        await _serviceManager.CommentService.UpdateComment(user.Id, commentId, commentDto);

        _response = new ApiResponse("Comment updated Succesfully", true, null, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);

    }
    [Authorize(Roles = "User")]
    [HttpDelete("delete-comment/{commentId}")]
    public async Task<IActionResult> DeleteComment(int commentId)
    {
        var user = await _serviceManager.UserService.GetUserWithClaim(User);
        await _serviceManager.CommentService.DeleteComment(user.Id, commentId);

        _response = new ApiResponse("Comment deleted Succesfully", true, null, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);

    }



}
