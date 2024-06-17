using Client.Contracts;
using Client.Models;

namespace Client.Services;

public class UserService : IUserService
{
    private readonly IHttpRequestService<Result> _request;
    public UserService(IHttpRequestService<Result> request)
    {
        _request = request;
    }
    public async Task<ApiResponse<UserResult>> GetUserWithEmail(string email)
    {
        return await _request.SendAsync<UserResult>(new ApiRequest(ApiType.GET,$"User/with-email/{email}",null));
    }
    public async Task<ApiResponse<AuthorizedUserResult>> GetAuthorizedUserData(int id)
    {
        return await _request.SendAsync<AuthorizedUserResult>(new ApiRequest(ApiType.GET, $"User/with-id/{id}", null));
    }
    public async Task<ApiResponse<Result>> UpdateAuthrizedUserData(int id, AuthorizedUserResult authorizedUserResult)
    {
        return await _request.SendAsync<Result>(new ApiRequest(ApiType.PUT, $"User/with-id/{id}", authorizedUserResult));
    }
}
