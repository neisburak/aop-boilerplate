using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors.Castle;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching;

public class CacheRemoveAspect : MethodInterception
{
    private readonly string _pattern;
    private readonly ICacheManager _cacheManager;

    public CacheRemoveAspect(string pattern)
    {
        _pattern = pattern;
        var cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        _cacheManager = cacheManager ?? throw new ArgumentNullException(nameof(ICacheManager));
    }

    protected override void OnSuccess(IInvocation invocation)
    {
        _cacheManager.RemoveByPattern(_pattern);
    }
}