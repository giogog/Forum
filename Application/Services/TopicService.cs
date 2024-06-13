using AutoMapper;
using Contracts;
using Domain.Entities;
using Domain.Models;

namespace Application.Services;

public class TopicService : ITopicService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public TopicService(IRepositoryManager repositoryManager,IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TopicDto>> GetTopics()
    {
        var topics = await _repositoryManager.TopicRepository.GetAllTopicAsync();

        if (!topics.Any())
            throw new NotFoundException("Topics Not Found");


        return _mapper.Map<IEnumerable<TopicDto>>(topics);
    }

    public async Task<IEnumerable<TopicWithContentDto>> GetTopicsWithContent()
    {
        var topics = await _repositoryManager.TopicRepository.GetAllTopicWithContentAsync();

        if (!topics.Any())
            throw new NotFoundException("Topics Not Found");


        return _mapper.Map<IEnumerable<TopicWithContentDto>>(topics);
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


}
