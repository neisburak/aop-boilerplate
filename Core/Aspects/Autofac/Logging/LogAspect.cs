using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Constants;
using Core.Utilities.Interceptors.Castle;

namespace Core.Aspects.Autofac.Logging;

public class LogAspect : MethodInterception
{
    private readonly LoggerServiceBase _loggerServiceBase;

    public LogAspect(Type loggerServiceType)
    {
        if (!loggerServiceType.BaseType!.IsAssignableFrom(typeof(LoggerServiceBase)))
        {
            throw new System.Exception(AspectMessages.WrongLoggerTypeError);
        }

        var loggerServiceBase = Activator.CreateInstance(loggerServiceType) as LoggerServiceBase;
        _loggerServiceBase = loggerServiceBase ?? throw new ArgumentNullException(nameof(LoggerServiceBase));
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _loggerServiceBase.Info(GetLogDetail(invocation));
    }

    private LogDetail GetLogDetail(IInvocation invocation)
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

        var logDetail = new LogDetail
        {
            MethodName = invocation.Method.Name,
            LogParameters = logParameters
        };

        return logDetail;
    }
}