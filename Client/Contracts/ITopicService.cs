using Client.Models;

namespace Client.Contracts;

public interface ITopicService
{
    Task<ApiResponse<Result>> AddTopic(TopicModelDto createTopicModelDto);
    Task<ApiResponse<Result>> EditTopic(UpdateTopicModelDto createTopicModelDto);
    Task<ApiResponse<Result>> DeleteTopic(int topicId);
    Task<PaginatedApiResponse<IEnumerable<TopicWithContentResult>>> GetTopicsWithContent(int page);
    Task<ApiResponse<TopicWithContentResult>> GetTopicById(int id);
}
