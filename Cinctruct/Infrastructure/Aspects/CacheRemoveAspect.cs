using Castle.DynamicProxy;
using Infrastructure.CrossCuttingConcerns.Caching.Interfaces;
using Infrastructure.Utilities.Interceptors;
using Infrastructure.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Aspects
{
	/// <summary>
	/// Aspect for removing cached entries based on a specified pattern. It is triggered after a successful method execution.
	/// </summary>
	public class CacheRemoveAspect : MethodInterception
	{
		private string _pattern;
		private ICacheService _cacheManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheRemoveAspect"/> class with a pattern to remove from the cache.
		/// </summary>
		/// <param name="pattern">The pattern or key to match and remove from the cache.</param>
		public CacheRemoveAspect(string pattern)
		{
			_pattern = pattern;
			_cacheManager = ServiceTool.ServiceProvider.GetService<ICacheService>();
		}

		/// <summary>
		/// Removes cached entries that match the specified pattern after a successful method execution.
		/// </summary>
		/// <param name="invocation">The invocation object that contains information about the method being intercepted.</param>
		protected override void OnSuccess(IInvocation invocation)
		{
			if (_cacheManager.IsAdd(_pattern))
			{
				_cacheManager.Remove(_pattern);
			}
		}
	}
}
