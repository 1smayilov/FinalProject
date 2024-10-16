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
        public static string ProductAdded = "Produkt yükləndi";
        public static string ProductNameInvalid = "Produkt adı uyğun deyil";
        public static string MaintenanceTime = "Server temirdedir";
        public static string ProductsListed = "Produktlar listelendi";
        public static string ProductCountOfCategoryError = "Kategoriyada en chox 10 produkt ola biler";
        public static string ProductNameAlreadyExists = "Bu adda başqa bir produkt var";
        public static string CategoryLimitExceded = "Kategoriya limit aşıldığı üçün yeni produkt əlavə edə bilməzsiniz";
        public static string AuthorizationDenied = "Səlahiyyətiniz yoxdur";
        public static string UserRegistered = "Qeydiyyatdan keçdi";
        public static string UserNotFound = "İstifadəçi tapılmadı";
        public static string PasswordError = "Şifrə yanlışdır";
        public static string SuccessfulLogin = "Daxil oldunuz";
        public static string UserAlreadyExists = "Bu istifadəçi mövcuddur";
        public static string AccessTokenCreated = "Token yaradıldı";
    }
}
