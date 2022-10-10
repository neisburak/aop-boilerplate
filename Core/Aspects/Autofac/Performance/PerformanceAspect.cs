using System.Diagnostics;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Castle;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance;

public class PerformanceAspect : MethodInterception
{
    private readonly int _interval;
    private readonly Stopwatch _stopwatch;

    public PerformanceAspect(int interval)
    {
        _interval = interval;
        var stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        _stopwatch = stopwatch ?? throw new ArgumentNullException(nameof(Stopwatch));
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _stopwatch.Start();
    }

    protected override void OnAfter(IInvocation invocation)
    {
        if (_stopwatch.Elapsed.TotalSeconds > _interval)
        {
            Debug.WriteLine($"Performance : {invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name}() elapsed {_stopwatch.Elapsed.TotalSeconds} second(s).");
        }
        _stopwatch.Reset();
    }
}