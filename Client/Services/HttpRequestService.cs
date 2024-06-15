using Client.Contracts;
using Client.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
//using System.Text.Json;

namespace Client.Services;

public class HttpRequestService<T> : IHttpRequestService<T>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _url;
    public HttpRequestService(IHttpClientFactory httpClientFactory,IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _url = config["ApiUrl"];
    }
    public async Task<ApiResponse<T>> SendAsync<T>(ApiRequest request)
    {

        try
        {
            var loginAsJson = JsonConvert.SerializeObject(request.Data);
            var requestUrl = $"{_url}{request.Endpoint}";
            HttpClient client = _httpClientFactory.CreateClient("API");
            HttpRequestMessage message = new();

            message.RequestUri = new Uri(requestUrl);

            if (request.Data != null)
            {
                message.Content = new StringContent(loginAsJson, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage apiResponse = new();

            switch (request.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.PATCH:
                    message.Method = HttpMethod.Patch;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }


            apiResponse = await client.SendAsync(message);


            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<T>>(apiContent);
            return result;

        }
        catch (Exception ex)
        {
            ApiResponse<T> result = new()
            {
                Message = ex.Message,
                IsSuccess = false
            };

            return result;
        }
    }
}

