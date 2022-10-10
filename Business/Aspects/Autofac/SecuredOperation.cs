using Business.Abstract;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors.Castle;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Aspects.Autofac;

public class SecuredOperation : MethodInterception
{
    private readonly string[] _permissions;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public SecuredOperation(params string[] permissions)
    {
        _permissions = permissions;
        var httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(IHttpContextAccessor));

        var userService = ServiceTool.ServiceProvider.GetService<IUserService>();
        _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

        foreach (var role in _permissions)
        {
            if (roleClaims!.Contains(role))
            {
                return;
            }
        }

        throw new Exception(Messages.AuthorizationDenied);
    }
}