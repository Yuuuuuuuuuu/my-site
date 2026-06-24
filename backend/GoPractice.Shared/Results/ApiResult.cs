namespace GoPractice.Shared.Results;

public class ApiResult
{
    public int Code { get; init; }

    public string Message { get; init; } = string.Empty;

    public bool Success => Code == 0;

    public static ApiResult Ok(string message = "ok") => new()
    {
        Code = 0,
        Message = message
    };

    public static ApiResult Fail(string message, int code = -1) => new()
    {
        Code = code,
        Message = message
    };
}

public class ApiResult<T> : ApiResult
{
    public T? Data { get; init; }

    public static ApiResult<T> Ok(T? data, string message = "ok") => new()
    {
        Code = 0,
        Message = message,
        Data = data
    };

    public new static ApiResult<T> Fail(string message, int code = -1) => new()
    {
        Code = code,
        Message = message,
        Data = default
    };
}
