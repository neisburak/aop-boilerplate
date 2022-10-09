using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public IResult Add(User user)
    {
        _userDal.Add(user);
        return Result.Succeed(Messages.UserCreated);
    }

    public async Task<IResult> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _userDal.AddAsync(user, cancellationToken);
        return Result.Succeed(Messages.UserCreated);
    }

    public IDataResult<User?> GetByMail(string email)
    {
        var user = _userDal.Get(g => g.Email == email);
        if (user is null) return DataResult<User?>.Error();
        return DataResult<User?>.Result(user);
    }

    public async Task<IDataResult<User?>> GetByMailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userDal.GetAsync(g => g.Email == email, cancellationToken);
        if (user is null) return DataResult<User?>.Error();
        return DataResult<User?>.Result(user);
    }

    public IDataResult<IList<OperationClaim>> GetClaims(User user)
    {
        return DataResult<IList<OperationClaim>>.Result(_userDal.GetClaims(user));
    }

    public async Task<IDataResult<IList<OperationClaim>>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
    {
        return DataResult<IList<OperationClaim>>.Result(await _userDal.GetClaimsAsync(user, cancellationToken));
    }
}