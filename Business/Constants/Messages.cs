using Core.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    // Tek Bir Kopya: Statik sınıflar ve üyeler, uygulama boyunca sadece bir kez
    // oluşturulur ve bellekte tek bir kopya olarak tutulur. Bu, belleğin daha verimli kullanılmasını sağlar.

    // Kolay Erişim: Statik sınıflar, doğrudan sınıf adı ile erişilebilirler.Yani, Messages
    // sınıfının üyelerine erişmek için bir nesne oluşturmaya gerek yoktur. 
    public static class Messages   // static olanda new() - lənə bilmir
    {
        public static string ProductAdded = "Məhsullar yükləndi";
        public static string ProductNameInvalid = "Məhsul adı uyğun deyil";
        public static string ProductsListed = "Məhsullar listləndi";
        public static string ProductFetchedById = "Id yə uyğun məhsul gətirildi";
        public static string ProductsFetchedByCategoryId = "Kateqoriyaya uyğun məhsullar gətirildi";
        public static string ProductsFetchedByPriceRange = "Verilən qiymət aralığındakı məhsullar gətirildi";
        public static string ProductCountOfCategoryError = "Kateqoriyada en chox 10 məhsul ola biler";
        public static string ProductNameAlreadyExists = "Bu adda başqa bir məhsul var";

        public static string CategoryLimitExceded = "Kategoriya limiti aşıldığı üçün yeni məhsul əlavə edə bilməzsiniz";
        public static string CategoriesListed = "Kateqoriyalar listləndi";
        public static string CategoryFetchedById = "Id yə uyğun kateqoriya gətirildi";

        public static string MaintenanceTime = "Server temirdedir";

        public static string AuthorizationDenied = "Səlahiyyətiniz yoxdur";
        public static string UserRegistered = "Qeydiyyatdan keçdi";
        public static string UserNotFound = "İstifadəçi tapılmadı";
        public static string PasswordError = "Şifrə yanlışdır";
        public static string SuccessfulLogin = "Daxil oldunuz";
        public static string UserAlreadyExists = "Bu istifadəçi mövcuddur";
        public static string AccessTokenCreated = "Token yaradıldı";
    }
}
