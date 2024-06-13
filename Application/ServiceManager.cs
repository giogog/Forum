using Application.Services;
using AutoMapper;
using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthorizationService> _authorizationService;
    private readonly Lazy<ITopicService> _topicService;
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<ICommentService> _commentService;
    private readonly Lazy<IEmailService> _emailService;
    public ServiceManager(
        IRepositoryManager repositoryManager,
        RoleManager<Role> roleManager,
        ITokenGenerator tokenGenerator,
        IEmailSender emailSender,
        IMapper mapper
        )
    {
        _authorizationService = new (() => new AuthorizationService(roleManager, tokenGenerator, repositoryManager));
        _topicService = new(() => new TopicService(repositoryManager, mapper));
        _userService = new(() => new UserService(repositoryManager, mapper));
        _commentService = new(() => new CommentService(repositoryManager, mapper));
        _emailService = new(() => new EmailService(emailSender, repositoryManager, tokenGenerator));
    }
    public IAuthorizationService AuthorizationService => _authorizationService.Value;
    public ITopicService TopicService => _topicService.Value;
    public IUserService UserService => _userService.Value;
    public ICommentService CommentService => _commentService.Value;
    public IEmailService EmailService => _emailService.Value;
}
