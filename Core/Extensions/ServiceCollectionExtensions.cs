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

 