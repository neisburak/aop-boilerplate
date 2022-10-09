using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;

    public CategoryManager(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }

    public IDataResult<IList<Category>> Get()
    {
        return DataResult<IList<Category>>.Result(_categoryDal.GetList());
    }

    public async Task<IDataResult<IList<Category>>> GetAsync(CancellationToken cancellationToken)
    {
        return DataResult<IList<Category>>.Result(await _categoryDal.GetListAsync(cancellationToken));
    }
}