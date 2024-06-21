using Client.Models;

namespace Client.Contracts;

public interface ITopicService
{
    Task<ApiResponse<Result>> AddTopic(CreateTopicModelDto createTopicModelDto);
    Task<ApiResponse<IEnumerable<TopicWithContentResult>>> GetTopicsWithContent(int pageNum);
}
