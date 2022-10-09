using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return Result.Succeed(Messages.ProductAdded);
        }

        public async Task<IResult> AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _productDal.AddAsync(product, cancellationToken);
            return Result.Succeed(Messages.ProductAdded);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return Result.Succeed(Messages.ProductUpdated);
        }

        public async Task<IResult> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            await _productDal.UpdateAsync(product, cancellationToken);
            return Result.Succeed(Messages.ProductUpdated);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return Result.Succeed(Messages.ProductDeleted);
        }

        public async Task<IResult> DeleteAsync(Product product, CancellationToken cancellationToken)
        {
            await _productDal.DeleteAsync(product, cancellationToken);
            return Result.Succeed(Messages.ProductDeleted);
        }

        public IDataResult<Product?> Get(int id)
        {
            var product = _productDal.Get(g => g.ProductId == id);
            if(product is null) return DataResult<Product?>.Error();
            return DataResult<Product?>.Result(product);
        }

        public async Task<IDataResult<Product?>> GetAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productDal.GetAsync(g => g.ProductId == id, cancellationToken);
            if(product is null) return DataResult<Product?>.Error();
            return DataResult<Product?>.Result(product);
        }

        public IDataResult<IList<Product>> Get()
        {
            return DataResult<IList<Product>>.Result(_productDal.GetList());
        }

        public async Task<IDataResult<IList<Product>>> GetAsync(CancellationToken cancellationToken)
        {
            return DataResult<IList<Product>>.Result(await _productDal.GetListAsync(cancellationToken));
        }

        public IDataResult<IList<Product>> GetByCategoryId(int categoryId)
        {
            return DataResult<IList<Product>>.Result(_productDal.GetList(g => g.ProductId == categoryId));
        }

        public async Task<IDataResult<IList<Product>>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
        {
            return DataResult<IList<Product>>.Result(await _productDal.GetListAsync(g => g.ProductId == categoryId, cancellationToken));
        }
    }
}