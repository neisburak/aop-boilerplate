using Newtonsoft.Json;

namespace Core.Extensions.Exception;

public class ErrorDetails
{
    public string Message { get; set; } = default!;
    public int StatusCode { get; set; }

    public override string ToString() => JsonConvert.SerializeObject(this);
}