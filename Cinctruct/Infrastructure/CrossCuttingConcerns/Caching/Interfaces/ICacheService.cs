namespace Infrastructure.CrossCuttingConcerns.Caching.Interfaces
{
	/// <summary>
	/// Defines the contract for a caching service to manage in-memory cache operations.
	/// </summary>
	public interface ICacheService
	{
		/// <summary>
		/// Adds an item to the cache with the specified key and duration.
		/// </summary>
		/// <param name="key">The key used to identify the cached item.</param>
		/// <param name="value">The object to cache.</param>
		/// <param name="duration">The duration (in minutes) for which the item should remain in the cache.</param>
		void Add(string key, object value, int duration);

		/// <summary>
		/// Determines whether an item exists in the cache by the given key.
		/// </summary>
		/// <param name="key">The key to check in the cache.</param>
		/// <returns>True if the item exists in the cache; otherwise, false.</returns>
		bool IsAdd(string key);

		/// <summary>
		/// Retrieves an item from the cache with the specified key and returns it as the specified type.
		/// </summary>
		/// <typeparam name="T">The type to cast the cached item to.</typeparam>
		/// <param name="key">The key used to retrieve the cached item.</param>
		/// <returns>The cached item cast to the specified type.</returns>
		T Get<T>(string key);

		/// <summary>
		/// Retrieves an item from the cache with the specified key.
		/// </summary>
		/// <param name="key">The key used to retrieve the cached item.</param>
		/// <returns>The cached object.</returns>
		object Get(string key);

		/// <summary>
		/// Removes an item from the cache with the specified key.
		/// </summary>
		/// <param name="key">The key used to identify the cached item for removal.</param>
		void Remove(string key);
	}
}
