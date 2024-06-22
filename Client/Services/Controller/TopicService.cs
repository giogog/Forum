using Client.Contracts;
using Client.Models;

namespace Client.Services;

public class TopicService : ITopicService
{
    private readonly IHttpRequestService<Result> _httpRequestService;

    public TopicService(IHttpRequestService<Result> httpRequestService)
    {
        _httpRequestService = httpRequestService;
    }

    public async Task<ApiResponse<Result>> AddTopic(TopicModelDto createTopicModelDto)
    {
    
        return await _httpRequestService.RequestAsync<Result>(new ApiRequest(ApiType.POST, $"Topic/create-topic", createTopicModelDto));
    }



    public async Task<ApiResponse<TopicWithContentResult>> GetTopicById(int id)
    {
        return await _httpRequestService.RequestAsync<TopicWithContentResult>(new ApiRequest(ApiType.GET, $"Topic/topic-with-content/{id}", null));
    }

    public async Task<PaginatedApiResponse<IEnumerable<TopicWithContentResult>>> GetTopicsWithContent(int page)
    {
        return await _httpRequestService.PaginatedRequestAsync<IEnumerable<TopicWithContentResult>>(new ApiRequest(ApiType.GET, $"Topic/topics-with-content/{page}",null));
    }

    public async Task<ApiResponse<Result>> EditTopic( UpdateTopicModelDto createTopicModelDto)
    {
        return await _httpRequestService.RequestAsync<Result>(new ApiRequest(ApiType.PUT, $"Topic/update-topic/{createTopicModelDto.Id}", createTopicModelDto));
    }
    public async Task<ApiResponse<Result>> DeleteTopic(int topicId)
    {
        return await _httpRequestService.RequestAsync<Result>(new ApiRequest(ApiType.DELETE, $"Topic/delete-topic/{topicId}", null));
    }
}
