using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    // IProductDal nə üçün lazımdır
    // Business IProductDal istifadə edir
    // İProductDal ın içərisinə ürüne ait özəl operasyonları qoyacıyıq
    // Ürünün detaylarını gətirmək üçün Ürünle Categorye join atmak 
    // İProduct dalın içərisində yazacağın özəl operasyon üçün, ancaq EfProductDal deyəcək ki məni implement et, o birilər deməyəcək
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // Ürünlerle kateqoriyanı join et
                var result = from p in context.Products
                             join c in context.Categories
                             // neye gore join yapayim
                             on p.CategoryId equals c.CategoryId
                             // sonucu su kolonlara uydurarak ver
                             select new ProductDetailDto 
                             {
                                 ProductId = p.ProductId, 
                                 ProductName = p.ProductName, 
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }
        }
    }
}
