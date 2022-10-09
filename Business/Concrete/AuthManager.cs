using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security;
using Entities.Dto;

namespace Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        throw new NotImplementedException();
    }

    public IDataResult<User> Login(UserForLogin userForLogin)
    {
        var userToCheck = _userService.GetByMail(userForLogin.Email);
        if(userToCheck is null) return DataResult<User>.Error(Messages.UserNotFound);
        
    }

    public IDataResult<User> Register(UserForRegister userForRegister)
    {
        throw new NotImplementedException();
    }

    public IResult UserExists(string email)
    {
        throw new NotImplementedException();
    }
}