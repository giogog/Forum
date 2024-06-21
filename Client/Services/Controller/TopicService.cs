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

    public async Task<ApiResponse<Result>> AddTopic(CreateTopicModelDto createTopicModelDto)
    {
    
        return await _httpRequestService.RequestAsync<Result>(new ApiRequest(ApiType.POST, $"Topic/create-topic", createTopicModelDto));
    }

    public async Task<PaginatedApiResponse<IEnumerable<TopicWithContentResult>>> GetTopicsWithContent(int page)
    {
        return await _httpRequestService.PaginatedRequestAsync<IEnumerable<TopicWithContentResult>>(new ApiRequest(ApiType.GET, $"Topic/with-content/{page}",null));
    }
}
