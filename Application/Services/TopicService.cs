using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class TopicService : ITopicService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly int _pageSize;
    public TopicService(IRepositoryManager repositoryManager,IMapper mapper,IConfiguration configuration)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _pageSize = Int32.Parse(configuration["AppSettings:PageSize"]);
    }
    public async Task<PagedList<TopicDto>> GetTopics(int page)
    {
        var topicDtos = _repositoryManager.TopicRepository.Topics()
            .Include(t => t.Comments)
            .ThenInclude(c => c.User)
            .Include(t => t.User)
            .Select(t => new TopicDto
            {
                Title = t.Title,
                Username = t.User.UserName,
                AuthorFullName = $"{t.User.Name} {t.User.Surname}",
                CommentNum = t.CommentNum,
                Created = t.Created,
                Id = t.Id,
                State = t.State,
                Status = t.Status
            }).OrderByDescending(t => t.Status == Status.Active);


        if (!topicDtos.Any())
            throw new NotFoundException("Topics Not Found");


        return await PagedList<TopicDto>.CreateAsync(topicDtos, page, _pageSize);
    }

    public async Task<PagedList<TopicWithContentDto>> GetTopicsWithContent(int page)
    {
        var topicDtos = _repositoryManager.TopicRepository.Topics()
            .Include(t => t.Comments)
            .ThenInclude(c => c.User)
            .Include(t => t.User)
            .Select(t => new TopicWithContentDto
            {
                Title = t.Title,
                Username = t.User.UserName,
                AuthorFullName = $"{t.User.Name} {t.User.Surname}",
                Body = t.Body,
                CommentNum = t.CommentNum,
                Comments = _mapper.Map<IEnumerable<CommentDto>>(t.Comments),
                Created = t.Created,
                Id = t.Id,
                UserId = t.UserId,
                Status = t.Status
            }).OrderByDescending(t => t.Status == Status.Active);


        if (!topicDtos.Any())
            throw new NotFoundException("Topics Not Found");
 

        return await PagedList<TopicWithContentDto>.CreateAsync(topicDtos, page, _pageSize);
    }

    public async Task CreateTopic(int userId,CreateTopicDto createTopicDto)
    {
        var topic = _mapper.Map<Topic>(createTopicDto);
        topic.UserId = userId;
        _repositoryManager.TopicRepository.AddTopictAsync(topic);

        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateTopic(int topicId,UpdateTopicDto createTopicDto)
    {
       
        var topic = await _repositoryManager.TopicRepository.GetTopicByIdAsync(topicId);
        if (topic.Status == Status.Inactive)
            throw new RestrictedException("You cant comment on this post");
        if (topic == null)
            throw new NotFoundException("Topic not found");

        topic.Body = createTopicDto.Body;
        topic.Title = createTopicDto.Title;

        _repositoryManager.TopicRepository.UpdateTopicAsync(topic);

        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteTopic(int topicId)
    {
        var topic = await _repositoryManager.TopicRepository.GetTopicByIdAsync(topicId);
        if (topic.Status == Status.Inactive)
            throw new RestrictedException("You cant comment on this post");
        if (topic == null)
            throw new NotFoundException("Topic not found");

        _repositoryManager.TopicRepository.DeleteTopicAsync(topic);

        await _repositoryManager.SaveAsync();
    }
    public async Task ChangeTopicState(int topicId, State state)
    {
        var topic = await _repositoryManager.TopicRepository.GetTopicByIdAsync(topicId);
        if (topic == null)
            throw new NotFoundException("Topic not found");
        if (state == State.Pending)
            throw new InvalidArgumentException("Can't Set this state");

        topic.State = state;
        _repositoryManager.TopicRepository.UpdateTopicAsync(topic);

        await _repositoryManager.SaveAsync();
    }
    public async Task ChangeTopicStatus(int topicId,Status status)
    {
        var topic = await _repositoryManager.TopicRepository.GetTopicByIdAsync(topicId);
        if (topic == null)
            throw new NotFoundException("Topic not found");

        topic.Status = status;


        _repositoryManager.TopicRepository.UpdateTopicAsync(topic);

        await _repositoryManager.SaveAsync();
    }

    public async Task<TopicWithContentDto> GetSingleTopicWithContent(int topicId)
    {
        var topic = await _repositoryManager.TopicRepository.GetTopicWithContentByIdAsync(topicId);
        if (topic == null)
            throw new NotFoundException("Topic not found");
        return _mapper.Map<TopicWithContentDto>(topic);
    }
}
