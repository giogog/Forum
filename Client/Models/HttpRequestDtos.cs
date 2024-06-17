namespace Client.Models;

public class ApiRequest(ApiType type, string endpoint, object data)
{
    public ApiType ApiType { get; set; } = type;
    public string Endpoint { get; set; } = endpoint;
    public object Data { get; set; } = data;
    public string? AccessToken { get; set; }
}


public record ApiResponse<T>
{
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
    public T? Result { get; set; }
    public int StatusCode { get; set; }
}

