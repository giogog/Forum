using Domain.Models;

namespace Contracts;

public interface ICommentService
{
    Task CreateComment(int UserId,CreateCommentDto commentDto);
    Task UpdateComment(int commentId, UpdateCommentDto commentDto);
    Task DeleteComment(int commentId);
}
