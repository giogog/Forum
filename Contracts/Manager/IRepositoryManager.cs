namespace Contracts;

public interface IRepositoryManager
{
    IUserRepository UserRepository { get; }
    ICommentRepository CommentRepository { get; }
    ITopicRepository TopicRepository { get; }
    Task SaveAsync();
}
