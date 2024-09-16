using Castle.DynamicProxy;
using Infrastructure.CrossCuttingConcerns.Caching.Interfaces;
using Infrastructure.Utilities.Interceptors;
using Infrastructure.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Aspects
{
	/// <summary>
	/// Aspect for caching the results of method executions. If the result of a method call is already cached, 
	/// it retrieves the result from the cache instead of executing the method again.
	/// </summary>
	public class CacheAspect : MethodInterception
	{
		private int _duration;
		private ICacheService _cacheManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheAspect"/> class with an optional cache duration.
		/// </summary>
		/// <param name="duration">The duration in minutes for which the cache entry should be stored. Default is 10 minutes.</param>
		public CacheAspect(int duration = 10)
		{
			_duration = duration;
			_cacheManager = ServiceTool.ServiceProvider.GetService<ICacheService>();
		}

		/// <summary>
		/// Intercepts method calls, checks if the result is already cached, and if so, returns the cached result.
		/// Otherwise, it proceeds with the method execution and caches the result.
		/// </summary>
		/// <param name="invocation">The invocation object that contains information about the method being intercepted.</param>
		public override void Intercept(IInvocation invocation)
		{
			var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
			var arguments = invocation.Arguments.ToList();
			var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
			if (_cacheManager.IsAdd(key))
			{
				invocation.ReturnValue = _cacheManager.Get(key);
				return;
			}
			invocation.Proceed();
			_cacheManager.Add(key, invocation.ReturnValue, _duration);
		}
	}
}
