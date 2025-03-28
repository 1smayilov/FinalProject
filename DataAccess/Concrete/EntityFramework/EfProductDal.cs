﻿using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    // IProductDal - Business layer tərəfindən istifadə olunur
    // IProductDal daxilində, yalnız məhsullara aid spesifik əməliyyatlar olacaq
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {


        public async Task<List<ProductDetailDto>> GetProductDetailsAsync()
        {
            using (var context = new NorthwindContext())
            {

                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };

                return await result.ToListAsync();
            }
        }
    }
}
