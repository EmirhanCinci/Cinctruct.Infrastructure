using Castle.DynamicProxy;
using Infrastructure.Utilities.Interceptors;
using Infrastructure.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Infrastructure.Aspects
{
	/// <summary>
	/// An aspect that measures and logs the performance of a method based on its execution time.
	/// </summary>
	public class PerformanceAspect : MethodInterception
	{
		private readonly int _interval;
		private readonly Stopwatch _stopwatch;

		/// <summary>
		/// Initializes a new instance of the <see cref="PerformanceAspect"/> class with a specified time interval.
		/// </summary>
		/// <param name="interval">The time interval in seconds to log the performance if exceeded.</param>
		public PerformanceAspect(int interval)
		{
			_interval = interval;
			_stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
		}

		/// <summary>
		/// Called before the method execution to start measuring performance.
		/// </summary>
		/// <param name="invocation">The invocation context for the method being intercepted.</param>
		protected override void OnBefore(IInvocation invocation)
		{
			_stopwatch.Start();
		}

		/// <summary>
		/// Called after the method execution to check and log the performance if it exceeds the specified interval.
		/// </summary>
		/// <param name="invocation">The invocation context for the method being intercepted.</param>
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
