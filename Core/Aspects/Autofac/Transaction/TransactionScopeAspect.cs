using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Transactions;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Asinxron metodları idarə etmək üçün await istifadə edirik
                    invocation.Proceed();  // Sinxron metodu icra edirik

                    // Əgər metod asinxron bir metoddursa
                    if (invocation.ReturnValue is Task task)
                    {
                        task.GetAwaiter().GetResult();  // Asinxron əməliyyatın başa çatmasını gözləyirik
                    }

                    transactionScope.Complete(); // Əgər hər şey uğurludursa, əməliyyatı tamamla
                }
                catch (Exception)
                {
                    transactionScope.Dispose(); // Hər hansı bir xəta baş verərsə, əməliyyatları geri al
                    throw;
                }
            }
        }
    }
}
// Metod işə düşdü bu "Database in Ramında" tutulur hələki əgər əməliyyat uğurlu olarsa
// database ə əlavə olunur yoxsa geri qayıdır
