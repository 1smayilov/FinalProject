using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module // Biz run verdiyimiz vaxt birinci bura işləyir
    {
        protected override void Load(ContainerBuilder builder) // Xəritə işi görür, nə soruşulanda nə işləsin // 1.
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


            // Buradakı, hazırda işləyən bütün sinifləri və interface ləri topla (ProductManager>().As<IProductService>)
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();  

                // Onların içində implement edilən interface ləri tap
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() 
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    // Onlar üçün AspectInterceptorSelector() - u çağır,
                    // baxsın görsün metodun üstündə atribut var?
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance(); 

            // AspectInterceptorSelector() - Bu Metodun üstündəki atributu yoxlayacaq

        }
    }
}
