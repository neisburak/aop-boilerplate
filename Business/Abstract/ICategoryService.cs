using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface ICategoryService
{
    IDataResult<IList<Category>> Get();
    Task<IDataResult<IList<Category>>> GetAsync(CancellationToken cancellationToken);
}
