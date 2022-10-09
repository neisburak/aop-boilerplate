using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IProductService
{
    IResult Add(Product product);
    Task<IResult> AddAsync(Product product, CancellationToken cancellationToken);

    IResult Delete(Product product);
    Task<IResult> DeleteAsync(Product product, CancellationToken cancellationToken);

    IResult Update(Product product);
    Task<IResult> UpdateAsync(Product product, CancellationToken cancellationToken);

    IDataResult<Product?> Get(int id);
    Task<IDataResult<Product?>> GetAsync(int id, CancellationToken cancellationToken);

    IDataResult<IList<Product>> Get();
    Task<IDataResult<IList<Product>>> GetAsync(CancellationToken cancellationToken);

    IDataResult<IList<Product>> GetByCategoryId(int categoryId);
    Task<IDataResult<IList<Product>>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken);
}