using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Abstract;

public interface IUserDal : IEntityRepository<User>
{
    IList<OperationClaim> GetClaims(User user);
    Task<IList<OperationClaim>> GetClaimsAsync(User user, CancellationToken cancellationToken);
}