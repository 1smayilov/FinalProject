using Autofac.Features.Metadata;
using Castle.DynamicProxy;
using System.Drawing;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    // Metodların icrasına müdaxilə etmək üçün istifadə olunur. Bu interfeýs, metodların əvvəlində və ya sonunda xüsusi kodların icrasına imkan verir.
    // Bu sinif IInterceptor interfeýsini tətbiq edir və metodların icrası zamanı müdaxilə etmək imkanı verir.
    // Priority xüsusiyyəti, interceptorların icra sırasını təyin edir.
    // IInterceptor Castle DynamicProxy kitabxanasının bir interfeýsidir
    {
        public int Priority { get; set; } // Atributların sırası

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }

}

