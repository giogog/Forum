using Domain.Models;

namespace Contracts;

public interface ITopicService
{
    Task<TopicWithContentDto> GetSingleTopicWithContent(int topicId);
    Task<PagedList<TopicDto>> GetTopics(int page);
    Task<PagedList<TopicWithContentDto>> GetTopicsWithContent(int page);
    Task CreateTopic(int userId,CreateTopicDto createTopicDto);
    Task UpdateTopic(int topicId, UpdateTopicDto updateTopicDto);
    Task DeleteTopic(int topicId);
    Task ChangeTopicState(int topicId, State state);
    Task ChangeTopicStatus(int topicId, Status status);
}
