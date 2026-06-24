namespace GoPractice.Shared.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message, int code = 4000) : base(message)
    {
        Code = code;
    }

    public int Code { get; }
}
