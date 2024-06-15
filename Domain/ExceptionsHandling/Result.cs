using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Exception;

public class Result<T>
{
    public string Message { get; private set; }
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }

    public static Result<T> Failed(string massage)
    {
        return new Result<T> { Message = massage, IsSuccess = false };
    }
    public static Result<T> Success(T? data, string massage = "") =>
        new Result<T> { Data = data, Message = massage, IsSuccess = true };

}
