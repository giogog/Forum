using Client.Contracts;
using Client.Models;

namespace Client.Services;

public class CommentService:ICommentService
{
    private readonly IHttpRequestService<Result> _httpRequestService;

    public CommentService(IHttpRequestService<Result> httpRequestService)
    {
        _httpRequestService = httpRequestService;
    }

    public async Task<ApiResponse<Result>> AddComment(AddCommentModelDto addCommentModelDto)
    {
        return await _httpRequestService.RequestAsync<Result>(new ApiRequest(ApiType.POST, "Comment",addCommentModelDto));
    }

    public async Task<ApiResponse<Result>> DeleteComment(int commentId)
    {
        return await _httpRequestService.RequestAsync<Result>(new ApiRequest(ApiType.DELETE, $"Comment/delete-comment/{commentId}", null));
    }

    public async Task<ApiResponse<Result>> EditComment(int commentId, UpdateCommentModelDto addCommentModelDto)
    {
        return await _httpRequestService.RequestAsync<Result>(new ApiRequest(ApiType.PUT, $"Comment/update-comment/{commentId}", addCommentModelDto));
    }
}
