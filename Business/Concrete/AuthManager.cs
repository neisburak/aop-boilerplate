using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security;
using Core.Utilities.Security.Hashing;
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

    public IDataResult<User?> Login(UserForLogin userForLogin)
    {
        var userToCheck = _userService.GetByMail(userForLogin.Email);
        if (!userToCheck.Success) return DataResult<User?>.Error(Messages.UserNotFound);

        if (!HashingHelper.VerifyPasswordHash(userForLogin.Password, userToCheck.Data!.PasswordHash, userToCheck.Data!.PasswordSalt))
        {
            return DataResult<User?>.Error(Messages.UserCredentialsError);
        }

        return DataResult<User?>.Result(userToCheck.Data);
    }

    public IDataResult<User?> Register(UserForRegister userForRegister)
    {
        HashingHelper.CreatePasswordHash(userForRegister.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = new User
        {
            Email = userForRegister.Email,
            FirstName = userForRegister.FirstName,
            LastName = userForRegister.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true
        };
        _userService.Add(user);
        return DataResult<User?>.Result(user, Messages.UserCreated);
    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = _userService.GetClaims(user);
        var accessToken = _tokenHelper.CreateToken(user, claims.Data!);
        return DataResult<AccessToken>.Result(accessToken);
    }

    public IResult UserExists(string email)
    {
        if (_userService.GetByMail(email) is null)
        {
            return Result.Failed(Messages.UserNotFound);
        }
        return Result.Succeed(Messages.UserExists);
    }
}