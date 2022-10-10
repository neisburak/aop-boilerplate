using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Constants;
using Core.Utilities.Interceptors.Castle;

namespace Core.Aspects.Autofac.Exception;

public class ExceptionLogAspect : MethodInterception
{
    private readonly LoggerServiceBase _loggerServiceBase;

    public ExceptionLogAspect(Type loggerServiceType)
    {
        if (!loggerServiceType.BaseType!.IsAssignableFrom(typeof(LoggerServiceBase)))
        {
            throw new System.Exception(AspectMessages.WrongLoggerTypeError);
        }

        var loggerServiceBase = Activator.CreateInstance(loggerServiceType) as LoggerServiceBase;
        _loggerServiceBase = loggerServiceBase ?? throw new ArgumentNullException(nameof(LoggerServiceBase));
    }

    protected override void OnException(IInvocation invocation, System.Exception e)
    {
        var logDetailWithException = GetLogDetail(invocation, e);
        _loggerServiceBase.Error(logDetailWithException);
    }

    private LogDetailWithException GetLogDetail(IInvocation invocation, System.Exception e)
    {
        var logParameters = new List<LogParameter>();
        for (int i = 0; i < invocation.Arguments.Length; i++)
        {
            logParameters.Add(new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name!,
                Value = invocation.Arguments[i],
                Type = invocation.Arguments[i].GetType().Name
            });
        }

        var logDetailWithException = new LogDetailWithException
        {
            MethodName = invocation.Method.Name,
            LogParameters = logParameters,
            ExceptionMessage = e.Message
        };

        return logDetailWithException;
    }
}