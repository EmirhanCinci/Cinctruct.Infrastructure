using Infrastructure.Model.Entities.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Interfaces
{
	/// <summary>
	/// Defines the contract for a repository for an entity of type <typeparamref name="TEntity"/> with identifier type <typeparamref name="TEntityId"/>.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <typeparam name="TEntityId">The type of the entity's identifier.</typeparam>
	public interface IBaseRepository<TEntity, TEntityId> : IAsyncRepository<TEntity, TEntityId>, IRepository<TEntity, TEntityId> where TEntity : IEntity<TEntityId>, new()
	{
		/// <summary>
		/// Retrieves a queryable collection of entities that match the specified criteria.
		/// </summary>
		/// <param name="predicate">The criteria to match.</param>
		/// <param name="enableTracking">Specifies whether tracking is enabled.</param>
		/// <param name="isDeleted">Optional flag to filter by deleted status.</param>
		/// <param name="includeList">Optional list of related entities to include.</param>
		/// <returns>A queryable collection of entities that match the criteria.</returns>
		IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null, params string[] includeList);
	}
}
