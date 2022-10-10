using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC;

public static class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

    public static IServiceCollection Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();

        return services;
    }
}