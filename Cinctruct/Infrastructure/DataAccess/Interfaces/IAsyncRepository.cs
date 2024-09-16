using Infrastructure.Model.Entities.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Interfaces
{
	/// <summary>
	/// Defines the contract for an asynchronous repository for an entity of type <typeparamref name="TEntity"/> with identifier type <typeparamref name="TEntityId"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
	public interface IAsyncRepository<TEntity, TEntityId> where TEntity : IEntity<TEntityId>, new()
	{
		/// <summary>
		/// Adds a new entity to the repository asynchronously.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <returns>The added entity.</returns>
		Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Adds a collection of entities to the repository asynchronously.
		/// </summary>
		/// <param name="entities">The collection of entities to add.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <returns>The added entities.</returns>
		Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);

		/// <summary>
		/// Determines whether any entity matches the specified criteria asynchronously.
		/// </summary>
		/// <param name="predicate">The criteria to match.</param>
		/// <param name="enableTracking">Specifies whether tracking is enabled.</param>
		/// <param name="isDeleted">Optional flag to filter by deleted status.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <returns><c>true</c> if any entity matches the criteria; otherwise, <c>false</c>.</returns>
		Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Counts the number of entities that match the specified criteria asynchronously.
		/// </summary>
		/// <param name="predicate">The criteria to match.</param>
		/// <param name="isDeleted">Optional flag to filter by deleted status.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <returns>The number of entities that match the criteria.</returns>
		Task<long> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool? isDeleted = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deletes the specified entity asynchronously.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		/// <param name="permanent">Specifies whether the deletion is permanent.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task DeleteAsync(TEntity entity, bool permanent = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// Deletes a collection of entities asynchronously.
		/// </summary>
		/// <param name="entities">The collection of entities to delete.</param>
		/// <param name="permanent">Specifies whether the deletion is permanent.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieves an entity that matches the specified criteria asynchronously.
		/// </summary>
		/// <param name="predicate">The criteria to match.</param>
		/// <param name="enableTracking">Specifies whether tracking is enabled.</param>
		/// <param name="isDeleted">Optional flag to filter by deleted status.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <param name="includeList">Optional list of related entities to include.</param>
		/// <returns>The entity that matches the criteria or <c>null</c> if no match is found.</returns>
		Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default, params string[] includeList);

		/// <summary>
		/// Retrieves an entity by its identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier of the entity.</param>
		/// <param name="enableTracking">Specifies whether tracking is enabled.</param>
		/// <param name="isDeleted">Optional flag to filter by deleted status.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <param name="includeList">Optional list of related entities to include.</param>
		/// <returns>The entity with the specified identifier or <c>null</c> if not found.</returns>
		Task<TEntity?> GetByIdAsync(TEntityId id, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default, params string[] includeList);

		/// <summary>
		/// Updates the specified entity asynchronously.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Updates a collection of entities asynchronously.
		/// </summary>
		/// <param name="entities">The collection of entities to update.</param>
		/// <param name="cancellationToken">Optional cancellation token.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
	}
}
