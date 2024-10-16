using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed(); // Metodu işlət
                    transactionScope.Complete(); // Metod uğurludur davam et (Cathce düşmür)
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose(); // Metod uğursuzdur hər şeyi geri qaytar
                    throw;
                }
            }
        }
    }
}

// Metod işə düşdü bu "Database in Ramında" tutulur hələki əgər əməliyyat uğurlu olarsa database ə əlavə olunur yoxsa geri qayıdır
