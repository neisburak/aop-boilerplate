using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete;

public class DataResult<T> : Result, IDataResult<T>
{
    protected DataResult(T? data, bool success, string message) : base(success, message)
    {
        Data = data;
    }
    protected DataResult(T? data, bool success) : base(success)
    {
        Data = data;
    }

    public T? Data { get; }

    public static IDataResult<T> Result() => new DataResult<T>(default, true);
    public static IDataResult<T> Result(T data) => new DataResult<T>(data, true);
    public static IDataResult<T> Result(string message) => new DataResult<T>(default, true, message);
    public static IDataResult<T> Result(T data, string message) => new DataResult<T>(data, true, message);

    public static IDataResult<T> Error() => new DataResult<T>(default, false);
    public static IDataResult<T> Error(T data) => new DataResult<T>(data, false);
    public static IDataResult<T> Error(string message) => new DataResult<T>(default, false, message);
    public static IDataResult<T> Error(T data, string message) => new DataResult<T>(data, false, message);
}