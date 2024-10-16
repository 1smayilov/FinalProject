using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); // Hansı CacheManageri istifadə etdiyimi göstərirəm
        }

        public override void Intercept(IInvocation invocation)
        {
            // Metodun Namespacini, IProductService-i (İnterfeysini) və metodun adını al
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            // Metodun parametrlərini listə çevir (...)
            var arguments = invocation.Arguments.ToList(); 
            // Metodun parametrlərini - əgər parametr dəyəri varsa GetAll() un içərisinə yüklə yoxdursa null qayıdacaq parametr 
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; // Business.Abstract.IProductService.GetAll() 
            // Get bax gör buna uyğun Ramda belə bir şey var? 
            if (_cacheManager.IsAdd(key))
            {
                // Əgər varsa Metodu heç işlətmədən geriyə qayıt dəyəri cache dən götür
                // ReturnValue - Metoddakı return ə deyir ki database dəki datanı keşdən çək, 
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            // Əgər yoxdursa metodu davam elətdir
            invocation.Proceed();
            // Keşə yüklə
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
            // ReturnValue - Core.Utilities.Results.SuccessDataResult<System.Collections.Generic.List<Entities.Concrete.Product>>
        }
    }
}
