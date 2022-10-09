using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserDal : EntityRepository<User, DataContext>, IUserDal
    {
        #region Helper Methods
        private IQueryable<OperationClaim> GetClaims(int userId)
        {
            using var context = new DataContext();
            return from operationClaim in context.OperationClaims
                   join userOperationClaim in context.UserOperationClaims on operationClaim.Id equals userOperationClaim.OperationClaimId
                   where userOperationClaim.UserId == userId
                   select operationClaim;
        }
        #endregion

        public IList<OperationClaim> GetClaims(User user)
        {
            return GetClaims(user.Id).ToList();
        }

        public async Task<IList<OperationClaim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
        {
            return await GetClaims(user.Id).ToListAsync(cancellationToken);
        }
    }
}