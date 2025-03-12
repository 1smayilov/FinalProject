using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    // Bəzi injectionlar var ki Autofacla birlikdə onları işlətmirik. (Core Module) un içinə bax 
    // Buna görə Asp.Net core öz injectionundan istifadə edirik.

    // Bu metod ASP.NET Core DI konteynerini qurur və ona çıxış təmin edir.
    // services.BuildServiceProvider() çağırıldıqda, bütün registrasiya olunan servis obyektləri yaradılır.
    // services geriyə qaytarılır ki, tətbiq normal işləsin.

    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services) 
        {
            ServiceProvider = services.BuildServiceProvider(); 
            return services;
        }

    // ServiceTool yalnız ASP.NET Core-un öz Dependency Injection(DI) mexanizmini istifadə edir.
    // Autofac-lə əlaqəli deyil və Autofac ilə qeydiyyatdan keçən servislərə çıxış təmin etmir.
    }
}


