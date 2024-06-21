using Domain.Models;

namespace Contracts;

public interface ITopicService
{
    Task<IEnumerable<TopicDto>> GetTopics();
    Task<IEnumerable<TopicWithContentDto>> GetTopicsWithContent(int pageNum);
    Task CreateTopic(int userId,CreateTopicDto createTopicDto);
    Task UpdateTopic(int topicId, UpdateTopicDto updateTopicDto);
    Task DeleteTopic(int topicId);
    Task ChangeTopicState(int topicId, State state);
    Task ChangeTopicStatus(int topicId, Status status);
}
