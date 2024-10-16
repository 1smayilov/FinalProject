using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    // JWT
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        // IHttpContextAccessor, sənin layihəndə istifadəçinin sorğusunu və məlumatlarını oxumağa və onlara əsaslanaraq müəyyən
        // məhdudiyyətləri tətbiq etməyə imkan verir. Bu, sistemin təhlükəsizliyini artırmaq üçün vacibdir ki, yalnız
        // lazımi rola sahib olan şəxslər müəyyən əməliyyatları həyata keçirə bilsinlər.

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); // vergülü sildi və 2 elementli arrayə atdı
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            // _httpContextAccessor - bu injection zəncirinin içində deyil, bunun o biri injectionları görməsi üçün ServiceTool yazırıq
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return; // İşlətməyə davam et
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}

// Təsəvvür et ki, sən bir ofisdə çalışırsan və sənə bir otaq açarı lazımdır.

// ServiceTool.ServiceProvider — Bu, ofisdəki bütün otaq açarlarının saxlandığı açar bankı kimidir. (Autofac servislər)
// GetService<IHttpContextAccessor>() — Bu, sənə xüsusi bir otaq açarını (burada HTTP kontekstini) tapmağa kömək edir.
// Sənin tətbiqdə _httpContextAccessor dəyişəni, HTTP kontekstini əldə etmək üçün istifadə olunur. Bu, sənə istifadəçi ilə bağlı məlumatları əldə etməyə imkan verir.

// Xülasə:
// Bu kod satırı, IHttpContextAccessor servisini ServiceTool.ServiceProvider vasitəsilə əldə edir ki,
// bu servis HTTP sorğuları ilə bağlı məlumatlara daxil olmaq imkanı yaradır.
// Bu məlumatlar, məsələn, istifadəçinin kimliyi və ya sessiya məlumatları ola bilər.



// İstifadəçi İdentifikasiyası:
// Sən mağazaya daxil olmaq üçün istifadəçi adı və parolunu daxil edirsən. Bu məlumatlar HTTP sorğusunda serverə göndərilir və server bu məlumatları yoxlayır.

// Sessiya Məlumatları:
// Mağazaya daxil olduqdan sonra, sən müxtəlif məhsulları seçə bilərsən. Bu seçkilər sessiya məlumatlarında saxlanılır ki, 
// sən səhifəni yenilədikdə belə bu məlumatlar saxlanılsın.

// Sorğu Parametrləri:
// Məhsul axtarışı zamanı axtarış kriteriyalarını URL-də (məsələn, search?query=laptop) təyin edirsən. Bu sorğu parametrləri serverə göndərilir.

// Kuki:
// Sənin brauzerində bir kuki saxlanılır ki, bu da serverə hər dəfə sorğu göndərdiyində istifadəçi məlumatlarını xatırlamağa kömək edir.