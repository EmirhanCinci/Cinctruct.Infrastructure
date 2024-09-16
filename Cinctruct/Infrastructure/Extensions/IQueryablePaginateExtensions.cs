using Infrastructure.Utilities.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
	/// <summary>
	/// Provides extension methods for pagination on <see cref="IQueryable{T}"/> sequences.
	/// </summary>
	public static class IQueryablePaginateExtensions
	{
		/// <summary>
		/// Converts an <see cref="IQueryable{T}"/> source to a paginated result asynchronously.
		/// </summary>
		/// <param name="source">The <see cref="IQueryable{T}"/> source to paginate.</param>
		/// <param name="index">The page index to retrieve (1-based).</param>
		/// <param name="size">The number of items per page.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A <see cref="Paginate{T}"/> containing paginated results.</returns>
		public static async Task<Paginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size, CancellationToken cancellationToken = default)
		{
			int count = await source.CountAsync(cancellationToken);
			List<T> items = await source.Skip((index - 1) * size).Take(size).ToListAsync(cancellationToken);
			Paginate<T> list = new() { Index = index, Count = count, Items = items, Size = size, Pages = (int)Math.Ceiling(count / (double)size) };
			return list;
		}

		/// <summary>
		/// Converts an <see cref="IQueryable{T}"/> source to a paginated result synchronously.
		/// </summary>
		/// <param name="source">The <see cref="IQueryable{T}"/> source to paginate.</param>
		/// <param name="index">The page index to retrieve (1-based).</param>
		/// <param name="size">The number of items per page.</param>
		/// <returns>A <see cref="Paginate{T}"/> containing paginated results.</returns>
		public static Paginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size)
		{
			int count = source.Count();
			var items = source.Skip((index - 1) * size).Take(size).ToList();
			Paginate<T> list = new() { Index = index, Size = size, Count = count, Items = items, Pages = (int)Math.Ceiling(count / (double)size) };
			return list;
		}
	}
}
