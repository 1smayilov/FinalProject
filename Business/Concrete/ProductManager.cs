﻿using Business.Abstract;
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
using System.Data;
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
        [PerformanceAspect(1)]

        public async Task<IDataResult<List<Product>>> GetAllAsync()
        {

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            var result = await _productDal.GetAllAsync();
            return new SuccessDataResult<List<Product>>(result, Messages.ProductsListed);
            // Console.WriteLine(result.Data.ProductName); // "Laptop"
        }


        public async Task<IDataResult<List<Product>>> GetAllByCategoryAsync(int categoryId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            var result = await _productDal.GetAllAsync(p => p.CategoryId == categoryId);
            return new SuccessDataResult<List<Product>>(result, Messages.ProductsFetchedByCategoryId);
        }


        public async Task<IDataResult<List<Product>>> GetUnitPriceAsync(decimal min, decimal max)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            var result = await _productDal.GetAllAsync(p=>p.UnitPrice >= min && p.UnitPrice <= max);
            return new SuccessDataResult<List<Product>>(result, Messages.ProductsFetchedByPriceRange);
        }

        public async Task<IDataResult<List<ProductDetailDto>>> GetProductDetailsAsync()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            var result = await _productDal.GetProductDetailsAsync();
            return new SuccessDataResult<List<ProductDetailDto>>(result, Messages.ProductsListed);
        }

        // Claim - Yetki

        [PerformanceAspect(1)]
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] // Sadəcə Get yazsan bütün servislərdəki getlərə aid olacaq (ki bu səhv yoldur)
        [TransactionScopeAspect]
        // IProductService.Get adını atributda göstərsəniz, bu metod icra edildikdən sonra,
        // IProductService.Get metodunun nəticəsi Ramdan silinir və növbəti dəfə bu metod çağırıldıqda yenidən məlumat bazasından əldə ediləcək.
        public async Task<IResult> AddAsync(Product product) // 3, 9
        {
           IResult result = await BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                                              CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                                              CheckIfCategoryLimitExceded());



            if (result != null)
            {
                return result; // result ın içi boş olmadığı üçün, artıq burada xəta mesajı gəlir
            }


            await _productDal.AddAsync(product);
            return new SuccessResult(Messages.ProductAdded); // 12
            
        }

        [CacheAspect]
        public async Task<IDataResult<Product>> GetByIdAsync(int productId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Product>(Messages.MaintenanceTime);
            }
            var result = await _productDal.GetAsync(p=>p.ProductId  == productId);
            return new SuccessDataResult<Product>(result, Messages.ProductFetchedById);
        }

        private async Task<IResult> CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            // IQueryable istifadə edirik ki, asinxron olaraq verilənlər bazasında say əməliyyatı aparsın
            var result = await _productDal.GetAllAsync(p => p.CategoryId == categoryId);

            if (result.Count > 100)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }

            return new SuccessResult();
        }


        private async Task<IResult> CheckIfProductNameExists(string productName)
        {
            var result = await _productDal.GetAllAsync(p => p.ProductName == productName); // Varmı? deməkdir - Bool qaytarır
            if (result.Any())
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private async Task<IResult> CheckIfCategoryLimitExceded()
        {
            var result = await _categoryService.GetAllAsync();
            if(result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded); 
            }
            return new SuccessResult();

        }

        //[TransactionScopeAspect]
        //public async Task<IResult> AddTransactionalAsync(Product product)
        //{
        //    await _productDal.AddAsync(product);
            
        //    await _productDal.AddAsync(product);

        //    return null;
        //}
    }
}
