using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete

// Sən _productDal vastəsilə product daldakı metodlara ulaşa bilirsən, onlardan istifadə edəcəksən
{
    // Bura iş kodlarıdır, istədiyi cürə metod yaza bilər
    public class ProductManager : IProductService
    {
        IProductDal _productDal; // Bağımlılığı minimaze edirəm
        ICategoryService _categoryService;
        // Niyə ICategoryDal yazmırıq - Entity öz Dalı xaric başqa bir dalı enjekte edə bilməzç səhvdir
        // Əgər ICategoryDal istifadə etsəniz, bu vəziyyətdə iş məntiqi birbaşa verilənlər bazası ilə qarışmış olacaq


        // Bağımlılığımı konstraktırla göstərirəm
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            // Mən Product manager olaraq veri erişim katmanına bağımlıyam

            // Mənə birdənə IProductDal referansı ver
            _productDal = productDal;
            _categoryService = categoryService;

        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
            // Data _productDal.GetAll() - dur
            // Console.WriteLine(result.Data.ProductName); // "Laptop"
        }


        public IDataResult<List<Product>> GetAllByCategory(int categoryId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId), Messages.ProductsListed);
        }


        public IDataResult<List<Product>> GetUnitPrice(decimal min, decimal max)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), Messages.ProductsListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductsListed);
        }

        // Claim - Yetki

        [PerformanceAspect(5)]
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] // Sadəcə Get yazsan bütün servislərdəki getlərə aid olacaq (ki bu səhv yoldur)
        [TransactionScopeAspect]
        // IProductService.Get adını atributda göstərsəniz, bu metod icra edildikdən sonra,
        // IProductService.Get metodunun nəticəsi Ramdan silinir və növbəti dəfə bu metod çağırıldıqda yenidən məlumat bazasından əldə ediləcək.
        public IResult Add(Product product) // 3, 9
        {
           IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                                              CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                                              CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded); // 12


            
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Product>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId), Messages.ProductsListed);
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count();
            if (result > 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any(); // Varmı? deməkdir - Bool qaytarır
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if(result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded); 
            }
            return new SuccessResult();

        }

        
    }
}
