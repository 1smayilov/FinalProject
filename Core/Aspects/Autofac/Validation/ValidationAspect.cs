using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Entities;
using Core.Utilities.Interceptors;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http.Headers;

namespace Core.Aspects.Autofac.Validation
{
    // bu classın məqsədi - metod çağırılmadan əvvəl doğrulama həyata keçirir
    // add metodunu doğrula ProductValidatoru istifadə edərək [ValidationAspect(typeof(ProductValidator))]
    public class ValidationAspect : MethodInterception // 4. // Bu bir interseptordur
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            // ILogger da ola bilər məsələn
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sinifi deyil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation) // 5.
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // ProductValidator //6.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // <Product>
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // (Product product) == <Product> // Çoxlu parametrlər də ola bilərdi
            foreach (var entity in entities) // Birdən çox parametr ola bilər deyə foreach
            {
                ValidationTool.Validate(validator, entity); //    ValidateTool - u burda işlədirik  // 7
            }
        }


//        Bu kod ValidationAspect adlı bir sinif təyin edir.Bu sinif metodlar çağırılmadan əvvəl doğrulama (validation) aparmaq üçün istifadə olunur.Bu sinifin necə işlədiyini və nə iş gördüyünü sadə şəkildə izah edim:


//        ValidationAspect Nədir?

//ValidationAspect: Bu sinif MethodInterception sinifindən miras alır. MethodInterception sinifi metod çağırışlarından əvvəl və sonra müəyyən əməliyyatları icra etməyə imkan verir.

//        Üzvlər və Əməliyyatlar:

//_validatorType: Bu, doğrulama üçün istifadə ediləcək sinifin tipidir. Məsələn, ProductValidator.

//        ValidationAspect(Type validatorType): Bu konstruktor validatorType parametrini qəbul edir:


//        validatorType: Doğrulama sinifinin tipi (məsələn, ProductValidator).

//typeof(IValidator).IsAssignableFrom(validatorType): Bu yoxlama validatorType-ın IValidator interfeysindən miras alıb-almadığını yoxlayır. Əgər almayıbsa, bir xəta atır.

//        OnBefore(IInvocation invocation): Bu metod metod çağırılmadan əvvəl işə düşür:


//        Activator.CreateInstance(_validatorType): Bu, validatorType-dan yeni bir obyekt yaradır (məsələn, ProductValidator).

//_validatorType.BaseType.GetGenericArguments()[0]: validatorType-ın BaseType-ından (əsas tipindən) generic (generic) parametrləri alır.Məsələn, AbstractValidator<Product>-dən Product tipini alır.

//invocation.Arguments.Where(t => t.GetType() == entityType): Metod çağırışı zamanı ötürülən bütün parametrləri əldə edir və yalnız entityType-a uyğun olanları seçir(məsələn, Product obyektləri).

//ValidationTool.Validate(validator, entity): Hər bir uyğun parametri(entity) validator ilə doğrulayır.

//Nə İş Görəcək?

//Bu sinif metodlar çağırılmadan əvvəl obyektlərin doğruluğunu yoxlayır:

//Doğrulama Sinifini Yoxlayır: Doğrulama sinifinin doğru olduğundan əmin olur(IValidator-dən miras almalıdır).

//Yeni Doğrulama Obyekti Yaradır: Doğrulama sinifindən bir obyekt yaradır.

//Parametrləri Seçir: Metod çağırışı zamanı ötürülən parametrlərdən yalnız uyğun olanlarını(məsələn, Product tipində olanları) seçir.

//Doğrulama Edir: Seçilmiş parametrləri yaradılmış doğrulama obyekti ilə yoxlayır.

//Beləliklə, metod çağırılmadan əvvəl obyektlərin düzgünlüyünü təmin edir.Bu, məsələn, Product obyektlərinin düzgün olduğunu təmin etmək üçün istifadə edilir.
    }
}
