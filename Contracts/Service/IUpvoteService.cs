namespace Contracts;

public interface IUpvoteService
{
    Task Upvote(int UserId, int TopicId);
    Task DownVote(int UserId, int TopicId);

}
