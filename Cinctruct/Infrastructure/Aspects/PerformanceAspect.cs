using Castle.DynamicProxy;
using Infrastructure.Utilities.Interceptors;
using Infrastructure.Utilities.IoC;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Aspects
{
    public class PerformanceAspect : MethodInterception
    {
        private readonly int _interval;
        private readonly Stopwatch _stopwatch;
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performans Durumu: {invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name} --> {_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        }
    }
}
