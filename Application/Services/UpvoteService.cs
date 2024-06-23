using Contracts;
using Domain.Entities;
using Domain.Exception;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class UpvoteService : IUpvoteService
{
    private readonly IRepositoryManager _repositoryManager;

    public UpvoteService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task Upvote(int userId, int topicId)
    {
        try
        {
            await _repositoryManager.BeginTransactionAsync();

            var upvoted = await _repositoryManager.UpvoteRepository.GetUpvote(u => u.UserId == userId && u.TopicId == topicId);
            if (upvoted != null)
            {
                //_logger.LogWarning("User {UserId} has already upvoted Topic {TopicId}", userId, topicId);
                //throw new RestrictedException("Already upvoted");
                await _repositoryManager.UpvoteRepository.DeleteUpvoteAsync(upvoted);
            }
            else
            {
                await _repositoryManager.UpvoteRepository.AddUpvoteAsync(new Upvote { UserId = userId, TopicId = topicId });
            }

            
            await _repositoryManager.SaveAsync();
            await _repositoryManager.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error occurred while upvoting Topic {TopicId} by User {UserId}", topicId, userId);
            await _repositoryManager.RollbackTransactionAsync();
            throw; // Re-throw the exception to propagate it to the caller
        }
    }

    public async Task DownVote(int userId, int topicId)
    {
        try
        {
            await _repositoryManager.BeginTransactionAsync();

            var upvoted = await _repositoryManager.UpvoteRepository.GetUpvote(u => u.UserId == userId && u.TopicId == topicId);
            if (upvoted == null)
            {
                //_logger.LogWarning("User {UserId} has not upvoted Topic {TopicId}", userId, topicId);
                throw new RestrictedException("Topic is not upvoted");
            }


            await _repositoryManager.SaveAsync();
            await _repositoryManager.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error occurred while downvoting Topic {TopicId} by User {UserId}", topicId, userId);
            await _repositoryManager.RollbackTransactionAsync();
            throw; // Re-throw the exception to propagate it to the caller
        }
    }
}

