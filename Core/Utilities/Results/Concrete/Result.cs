using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete;

public class Result : IResult
{
    protected Result(bool success, string message) : this(success)
    {
        Message = message;
    }

    protected Result(bool success)
    {
        Success = success;
    }

    public bool Success { get; }

    public string Message { get; } = default!;

    public static IResult Succeed() => new Result(true);
    public static IResult Succeed(string message) => new Result(true, message);
    public static IResult Failed() => new Result(false);
    public static IResult Failed(string message) => new Result(false, message);
}