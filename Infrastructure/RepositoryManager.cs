using Contracts;
using Domain.Entities;
using Infrastructure.DataConnection;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDataContext _context;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<ICommentRepository> _commentRepository;
    private readonly Lazy<ITopicRepository> _topicRepository;
    public RepositoryManager(ApplicationDataContext context,UserManager<User> userManager)
    {
        _context = context;
        _userRepository = new(() => new UserRepository(userManager));
        _commentRepository = new(() => new CommentRepository(context));
        _topicRepository = new(() => new TopicRepository(context));
    }
    public IUserRepository UserRepository => _userRepository.Value;
    public ICommentRepository CommentRepository => _commentRepository.Value;    
    public ITopicRepository TopicRepository => _topicRepository.Value;  
    public Task SaveAsync() => _context.SaveChangesAsync();
}
