using Castle.DynamicProxy;
using System;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        // MethodInterception sinifi, metod çağırışları zamanı xüsusi əməliyyatları yerinə yetirmək üçün genişlənə bilən bir bazadır.
        // OnBefore, OnAfter, OnException, və OnSuccess metodları, metodun müxtəlif mərhələlərində əlavə əməliyyatları həyata keçirmək üçün istifadə olunur.
        // Intercept metodu, əsl metodun çağırılmasını və müdaxilələrin idarə olunmasını təmin edir.
        // invocation : business methods
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation) // Mənim işlətmək istədiyim metoddur bu (add) 
        {
             
            var isSuccess = true;
            OnBefore(invocation); // 8.
            try
            {
                invocation.Proceed(); 
                // Əgər doğrulama uğurla keçirsə (ValidationTool), proxy metod çağırışını invocation.Proceed() vasitəsilə əsl metodu çağırır.
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation); // 10.
                }
            }
            
            OnAfter(invocation); // 11.
        }
    }
}


