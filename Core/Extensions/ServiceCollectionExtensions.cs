using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
        // Bu kodun məqsədi ICoreModule - u Polimorfizm istifadə edərək istifadə etmək 
        // Mən sabah CoreModule dan başqa bir Module da yaza bilərəm
        // Bu bütün injectionları bir araya toplayır
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}

// Müştəri restorana gələndə, menyudan istənilən yeməyi seçə bilər. Sən də aşpaza demək əvəzinə, sadəcə sifariş verirsən.
// Polimorfizm burada sənə kömək edir. Sənin üçün fərq etmir ki, müştəri pizza, makaron və ya şorba sifariş edir -
// sifariş hansı yemək olursa olsun, aşpaz onu hazırlayacaq. Yəni, müştəri hansı yeməyi sifariş etsə də, sən eyni qayda ilə aşpaza sifarişi ötürürsən.
