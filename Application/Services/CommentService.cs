using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Models;

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
    public CommentService()
    {
        
    }
    public async Task CreateComment(int userId, CreateCommentDto commentDto)
    {
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

        _repositoryManager.CommentRepository.AddCommentAsync(comment);

        await _repositoryManager.SaveAsync();
    }
    public async Task UpdateComment(int userId,int commentId,UpdateCommentDto commentDto)
    {
        var comment = await _repositoryManager.CommentRepository.GetCommentByIdAsync(commentId);
        if (comment.UserId != userId)
            throw new RestrictedException("You cant update this comment");
        if (comment == null)
            throw new NotFoundException("Comment Not Found");


        comment.Body = commentDto.Body;


        _repositoryManager.CommentRepository.UpdateCommentAsync(comment);
        await _repositoryManager.SaveAsync();
    }
    public async Task DeleteComment(int userId, int commentId)
    {
        var comment = await _repositoryManager.CommentRepository.GetCommentByIdAsync(commentId);
        if (comment.UserId != userId)
            throw new RestrictedException("You cant delete this comment");
        if (comment == null)
            throw new NotFoundException("Comment Not Found");

        _repositoryManager.CommentRepository.DeleteCommentAsync(comment);
        await _repositoryManager.SaveAsync();
    }
}
