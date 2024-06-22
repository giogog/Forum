using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public CommentService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task CreateComment(int userId, CreateCommentDto commentDto)
    {
        try
        {
            await _repositoryManager.BeginTransactionAsync();

            var topic = await _repositoryManager.TopicRepository.GetTopicByIdAsync(commentDto.TopicId);
            if (topic == null)
            {
                throw new NotFoundException("Topic Not Found");
            }

            if (topic.Status == Status.Inactive)
            {
                throw new RestrictedException("You can't comment on this post");
            }

            if (commentDto.ParentCommentId.HasValue)
            {
                var parentComment = await _repositoryManager.CommentRepository.GetCommentByIdAsync(commentDto.ParentCommentId.Value);
                if (parentComment == null)
                {
                    throw new NotFoundException("Parent Comment Not Found");
                }
            }

            var comment = _mapper.Map<Comment>(commentDto);
            comment.Type = commentDto.ParentCommentId.HasValue ? CommentType.Reply : CommentType.Comment;
            comment.UserId = userId;

            await _repositoryManager.CommentRepository.AddCommentAsync(comment);
            await _repositoryManager.SaveAsync();
            await _repositoryManager.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error occurred while creating comment for Topic {TopicId} by User {UserId}", commentDto.TopicId, userId);
            await _repositoryManager.RollbackTransactionAsync();
            throw; // Re-throw the exception to propagate it to the caller
        }
    }

    public async Task UpdateComment(int userId, int commentId, UpdateCommentDto commentDto)
    {
        try
        {
            await _repositoryManager.BeginTransactionAsync();

            var comment = await _repositoryManager.CommentRepository.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                throw new NotFoundException("Comment Not Found");
            }
            if (comment.UserId != userId)
            {
                throw new RestrictedException("You can't update this comment");
            }

            comment.Body = commentDto.Body;
            await _repositoryManager.CommentRepository.UpdateCommentAsync(comment);
            await _repositoryManager.SaveAsync();
            await _repositoryManager.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error occurred while updating comment {CommentId} by User {UserId}", commentId, userId);
            await _repositoryManager.RollbackTransactionAsync();
            throw; // Re-throw the exception to propagate it to the caller
        }
    }

    public async Task DeleteComment(int userId, int commentId)
    {
        try
        {
            await _repositoryManager.BeginTransactionAsync();

            var comment = await _repositoryManager.CommentRepository.GetCommentByIdAsync(commentId);
            if (comment == null)
            {
                throw new NotFoundException("Comment Not Found");
            }
            if (comment.UserId != userId)
            {
                throw new RestrictedException("You can't delete this comment");
            }

            await _repositoryManager.CommentRepository.DeleteCommentAsync(comment);
            await _repositoryManager.SaveAsync();
            await _repositoryManager.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error occurred while deleting comment {CommentId} by User {UserId}", commentId, userId);
            await _repositoryManager.RollbackTransactionAsync();
            throw; // Re-throw the exception to propagate it to the caller
        }
    }
}
