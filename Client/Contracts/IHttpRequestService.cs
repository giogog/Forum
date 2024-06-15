using Client.Models;

namespace Client.Contracts;

public interface IHttpRequestService<T>
{
    //Task<ApiResponse<T>> Get(string baseUrl, string endpoint,object data);
    //Task<ApiResponse<T>> Post(string baseUrl, string endpoint, object data);
    //Task<ApiResponse<T>> Put(string baseUrl, string endpoint, object data);
    //Task<ApiResponse<T>> Delete(string baseUrl, string endpoint, object data);

    Task<ApiResponse<T>> SendAsync<T>(ApiRequest request);
}
