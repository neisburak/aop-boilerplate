using Core.Entities.Concrete;

namespace Core.Utilities.Security;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);
}