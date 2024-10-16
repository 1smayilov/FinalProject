using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        // Bu metodun işləməsi 5 saniyəni keçərsə mənə xəbərdar et
        private int _interval; // Neçə saniyə
        private Stopwatch _stopwatch; // Timer - Bu metod nə kadar sürecek

        public PerformanceAspect(int interval)
        {
            _interval = interval; 
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }


        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start(); // Metodun əvvəlində timer başlayır
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval) // Keçən vaxt 5 saniyədən çoxdursa?
            { 
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset(); // Metodun sonunda Timer dayanır
        }
    }
}
