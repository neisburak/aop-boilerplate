using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security;
using Entities.Dto;

namespace Business.Abstract;

public interface IAuthService
{
    IDataResult<User> Register(UserForRegister userForRegister);
    IDataResult<User> Login(UserForLogin userForLogin);
    IResult UserExists(string email);
    IDataResult<AccessToken> CreateAccessToken(User user);
}