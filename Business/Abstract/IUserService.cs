using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract;

public interface IUserService
{
    IResult Add(User user);
    Task<IResult> AddAsync(User user, CancellationToken cancellationToken);

    IDataResult<User?> GetByMail(string email);
    Task<IDataResult<User?>> GetByMailAsync(string email, CancellationToken cancellationToken);


    IDataResult<IList<OperationClaim>> GetClaims(User user);
    Task<IDataResult<IList<OperationClaim>>> GetClaimsAsync(User user, CancellationToken cancellationToken);
}