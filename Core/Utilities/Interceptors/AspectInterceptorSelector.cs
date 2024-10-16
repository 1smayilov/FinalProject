using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
   // AspectInterceptorSelector sinifi, metodlara və siniflərə tətbiq olunmuş atributları(interceptorları) seçir.
   // Bu atributlar metodların icra olunma vaxtında(məsələn, öncə, sonra) və ya xəta baş verəndə xüsusi əməliyyatları yerinə yetirir.
   // SelectInterceptors metodu, metod və sinif səviyyəsində olan interceptorları seçərək qaytarır.
    public class AspectInterceptorSelector : IInterceptorSelector // 2.
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}

