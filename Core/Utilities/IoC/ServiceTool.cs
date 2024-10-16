using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services) 
        {
            ServiceProvider = services.BuildServiceProvider(); 
            return services;
        }
    }
    // Bundan Secured Options - da istifadə olunur
}

// İlkin Hazırlıq:
// Restoran açılmadan əvvəl, sənə xidmətçiləri qeyd etmək lazımdır: ofisiantlar, aşpazlar və s. Bu, IServiceCollection vasitəsilə həyata keçirilir.
// (bütün injectionlar)

// Restoranın Açılması:
// Restoran açıldıqda, sən Create metodunu çağırırsan. Bu metod, sənə qeyd etdiyin xidmətçiləri (xidmətləri) istifadə üçün hazır edir və
// ServiceProvider adlı mərkəzi qurur. (Hansının əvəzinə nə olmalıdır)

// Xidmətlərdən İstifadə:
// İndi müştərilər restoranın xidmətlərindən istifadə edə bilər. Əgər bir müştəri yemək sifariş edərsə, ServiceProvider istifadə edilərək müvafiq xidmətçilər (aşpazlar, ofisiantlar və s.) işə salınır və sifariş yerinə yetirilir.

