using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public async Task<IDataResult<List<Category>>> GetAllAsync()
        {
            var result = await _categoryDal.GetAllAsync();
            return new SuccessDataResult<List<Category>>(result, Messages.CategoriesListed);
        }

        public async Task<IDataResult<Category>> GetByIdAsync(int categoryId)
        {
            var result = await _categoryDal.GetAsync(c => c.CategoryId == categoryId);
            return new SuccessDataResult<Category>(result, Messages.CategoryFetchedById);
        }
    }
}
