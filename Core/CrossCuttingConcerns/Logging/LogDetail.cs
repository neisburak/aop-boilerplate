namespace Core.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public string MethodName { get; set; } = default!;
    public List<LogParameter> LogParameters { get; set; } = default!;
}