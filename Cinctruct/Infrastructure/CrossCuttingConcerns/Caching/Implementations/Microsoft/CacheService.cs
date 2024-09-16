using Infrastructure.CrossCuttingConcerns.Caching.Interfaces;
using Infrastructure.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CrossCuttingConcerns.Caching.Implementations.Microsoft
{
	/// <summary>
	/// Provides an implementation of the ICacheService interface for managing in-memory caching using IMemoryCache.
	/// </summary>
	public class CacheService : ICacheService
	{
		IMemoryCache _memoryCache;

		/// <summary>
		/// Initializes a new instance of the CacheService class and retrieves the IMemoryCache service.
		/// </summary>
		public CacheService()
		{
			_memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
		}

		/// <summary>
		/// Adds an object to the cache with the specified key and duration.
		/// </summary>
		/// <param name="key">The key used to store the object in the cache.</param>
		/// <param name="value">The object to store in the cache.</param>
		/// <param name="duration">The duration (in minutes) for which the object should be stored.</param>
		public void Add(string key, object value, int duration)
		{
			_memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
		}

		/// <summary>
		/// Retrieves an object from the cache with the specified key, cast to the specified type.
		/// </summary>
		/// <typeparam name="T">The type of the object to retrieve from the cache.</typeparam>
		/// <param name="key">The key used to retrieve the cached object.</param>
		/// <returns>The cached object cast to the specified type, or default if not found.</returns>
		public T Get<T>(string key)
		{
			return _memoryCache.Get<T>(key);
		}

		/// <summary>
		/// Retrieves an object from the cache with the specified key.
		/// </summary>
		/// <param name="key">The key used to retrieve the cached object.</param>
		/// <returns>The cached object, or null if not found.</returns>
		public object Get(string key)
		{
			return _memoryCache.Get(key);
		}

		/// <summary>
		/// Determines if an object is already cached with the specified key.
		/// </summary>
		/// <param name="key">The key to check in the cache.</param>
		/// <returns>True if the object is found in the cache; otherwise, false.</returns>
		public bool IsAdd(string key)
		{
			return _memoryCache.TryGetValue(key, out _);
		}

		/// <summary>
		/// Removes an object from the cache with the specified key.
		/// </summary>
		/// <param name="key">The key used to identify the object to remove from the cache.</param>
		public void Remove(string key)
		{
			_memoryCache.Remove(key);
		}
	}
}
