using Infrastructure.Model.Entities.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Interfaces
{
	/// <summary>
	/// Defines the contract for a repository for an entity of type <typeparamref name="TEntity"/> with identifier type <typeparamref name="TEntityId"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
	public interface IRepository<TEntity, TEntityId> where TEntity : IEntity<TEntityId>, new()
	{
		/// <summary>
		/// Adds a new entity to the repository.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		/// <returns>The added entity.</returns>
		TEntity Add(TEntity entity);

		/// <summary>
		/// Adds a collection of entities to the repository.
		/// </summary>
		/// <param name="entities">The collection of entities to add.</param>
		/// <returns>The added entities.</returns>
		ICollection<TEntity> AddRange(ICollection<TEntity> entities);

		/// <summary>
		/// Determines whether any entity matches the specified criteria.
		/// </summary>
		/// <param name="predicate">The criteria to match.</param>
		/// <param name="enableTracking">Specifies whether tracking is enabled.</param>
		/// <param name="isDeleted">Optional flag to filter by deleted status.</param>
		/// <returns><c>true</c> if any entity matches the criteria; otherwise, <c>false</c>.</returns>
		bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null);

		/// <summary>
		/// Counts the number of entities that match the specified criteria.
		/// </summary>
		/// <param name="predicate">The criteria to match.</param>
		/// <param name="isDeleted">Optional flag to filter by deleted status.</param>
		/// <returns>The number of entities that match the criteria.</returns>
		long Count(Expression<Func<TEntity, bool>>? predicate = null, bool? isDeleted = null);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		/// <param name="permanent">Specifies whether the deletion is permanent.</param>
		void Delete(TEntity entity, bool permanent = false);

		/// <summary>
		/// Deletes a collection of entities.
		/// </summary>
		/// <param name="entities">The collection of entities to delete.</param>
		/// <param name="permanent">Specifies whether the deletion is permanent.</param>
		void DeleteRange(ICollection<TEntity> entities, bool permanent = false);

		/// <summary>
		/// Retrieves an entity that matches the specified criteria.
		/// </summary>
		/// <param name="predicate">The criteria to match.</param>
		/// <param name="enableTracking">Specifies whether tracking is enabled.</param>
		/// <param name="isDeleted">Optional flag to filter by deleted status.</param>
		/// <param name="includeList">Optional list of related entities to include.</param>
		/// <returns>The entity that matches the criteria or <c>null</c> if no match is found.</returns>
		TEntity? Get(Expression<Func<TEntity, bool>> predicate, bool enableTracking = true, bool? isDeleted = null, params string[] includeList);

		/// <summary>
		/// Retrieves an entity by its identifier.
		/// </summary>
		/// <param name="id">The identifier of the entity.</param>
		/// <param name="enableTracking">Specifies whether tracking is enabled.</param>
		/// <param name="isDeleted">Optional flag to filter by deleted status.</param>
		/// <param name="includeList">Optional list of related entities to include.</param>
		/// <returns>The entity with the specified identifier or <c>null</c> if not found.</returns>
		TEntity? GetById(TEntityId id, bool enableTracking = true, bool? isDeleted = null, params string[] includeList);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(TEntity entity);

		/// <summary>
		/// Updates a collection of entities.
		/// </summary>
		/// <param name="entities">The collection of entities to update.</param>
		void UpdateRange(ICollection<TEntity> entities);
	}
}
