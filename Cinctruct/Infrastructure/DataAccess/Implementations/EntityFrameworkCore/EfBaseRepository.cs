using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Model.Entities.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Implementations.EntityFrameworkCore
{
	/// <summary>
	/// Base repository class for Entity Framework with common data access methods.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <typeparam name="TEntityId">The type of the entity identifier.</typeparam>
	/// <typeparam name="TContext">The type of the DbContext.</typeparam>
	public abstract class EfBaseRepository<TEntity, TEntityId, TContext> : IBaseRepository<TEntity, TEntityId> where TEntity : BaseEntity<TEntityId>, new() where TContext : DbContext
	{
		protected TContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="EfBaseRepository{TEntity, TEntityId, TContext}"/> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		protected EfBaseRepository(TContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Adds a new entity to the context.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		/// <returns>The added entity.</returns>
		public TEntity Add(TEntity entity)
		{
			entity.CreatedDate = DateTime.Now;
			_context.Set<TEntity>().Add(entity);
			return entity;
		}

		/// <summary>
		/// Asynchronously adds a new entity to the context.
		/// </summary>
		/// <param name="entity">The entity to add.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The task representing the asynchronous operation, with the added entity as the result.</returns>
		public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			entity.CreatedDate = DateTime.Now;
			await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
			return entity;
		}

		/// <summary>
		/// Adds a range of entities to the context.
		/// </summary>
		/// <param name="entities">The entities to add.</param>
		/// <returns>The added entities.</returns>
		public ICollection<TEntity> AddRange(ICollection<TEntity> entities)
		{
			foreach (TEntity entity in entities)
			{
				entity.CreatedDate = DateTime.Now;
			}
			_context.Set<TEntity>().AddRange(entities);
			return entities;
		}

		/// <summary>
		/// Asynchronously adds a range of entities to the context.
		/// </summary>
		/// <param name="entities">The entities to add.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The task representing the asynchronous operation, with the added entities as the result.</returns>
		public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
		{
			foreach (TEntity entity in entities)
			{
				entity.CreatedDate = DateTime.Now;
			}
			await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
			return entities;
		}

		/// <summary>
		/// Checks if any entities match the specified predicate.
		/// </summary>
		/// <param name="predicate">The predicate to filter entities.</param>
		/// <param name="enableTracking">Specifies if the entities should be tracked.</param>
		/// <param name="isDeleted">Specifies if the entities are deleted.</param>
		/// <returns>True if any entities match the predicate; otherwise, false.</returns>
		public bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null)
		{
			IQueryable<TEntity> queryable = !enableTracking ? _context.Set<TEntity>().AsNoTracking() : _context.Set<TEntity>();
			queryable = isDeleted.HasValue ? queryable.Where(prd => prd.IsDeleted == isDeleted) : queryable;
			return predicate != null ? queryable.Where(predicate).Any() : queryable.Any();
		}

		/// <summary>
		/// Asynchronously checks if any entities match the specified predicate.
		/// </summary>
		/// <param name="predicate">The predicate to filter entities.</param>
		/// <param name="enableTracking">Specifies if the entities should be tracked.</param>
		/// <param name="isDeleted">Specifies if the entities are deleted.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The task representing the asynchronous operation, with a boolean indicating if any entities match the predicate.</returns>
		public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default)
		{
			IQueryable<TEntity> queryable = !enableTracking ? _context.Set<TEntity>().AsNoTracking() : _context.Set<TEntity>();
			queryable = isDeleted.HasValue ? queryable.Where(prd => prd.IsDeleted == isDeleted) : queryable;
			return predicate != null ? await queryable.Where(predicate).AnyAsync(cancellationToken) : await queryable.AnyAsync(cancellationToken);
		}

		/// <summary>
		/// Gets the count of entities that match the specified predicate.
		/// </summary>
		/// <param name="predicate">The predicate to filter entities.</param>
		/// <param name="isDeleted">Specifies if the entities are deleted.</param>
		/// <returns>The count of entities that match the predicate.</returns>
		public long Count(Expression<Func<TEntity, bool>>? predicate = null, bool? isDeleted = null)
		{
			return _context.Set<TEntity>().Where(entity => !isDeleted.HasValue || entity.IsDeleted == isDeleted.Value).Count(predicate ?? (entity => true));
		}

		/// <summary>
		/// Asynchronously gets the count of entities that match the specified predicate.
		/// </summary>
		/// <param name="predicate">The predicate to filter entities.</param>
		/// <param name="isDeleted">Specifies if the entities are deleted.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The task representing the asynchronous operation, with the count of entities that match the predicate as the result.</returns>
		public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool? isDeleted = null, CancellationToken cancellationToken = default)
		{
			return await _context.Set<TEntity>().Where(entity => !isDeleted.HasValue || entity.IsDeleted == isDeleted.Value).CountAsync(predicate ?? (entity => true), cancellationToken);
		}

		/// <summary>
		/// Deletes the specified entity. Optionally, marks it as deleted without removing it permanently.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		/// <param name="permanent">Specifies if the entity should be removed permanently.</param>
		public void Delete(TEntity entity, bool permanent = false)
		{
			if (permanent)
			{
				_context.Set<TEntity>().Remove(entity);
			}
			else
			{
				entity.IsDeleted = true;
				entity.DeletedDate = DateTime.Now;
				Update(entity);
			}
		}

		/// <summary>
		/// Asynchronously deletes the specified entity. Optionally, marks it as deleted without removing it permanently.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		/// <param name="permanent">Specifies if the entity should be removed permanently.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The task representing the asynchronous operation.</returns>
		public async Task DeleteAsync(TEntity entity, bool permanent = false, CancellationToken cancellationToken = default)
		{
			if (permanent)
			{
				_context.Set<TEntity>().Remove(entity);
			}
			else
			{
				entity.IsDeleted = true;
				entity.DeletedDate = DateTime.Now;
				await UpdateAsync(entity, cancellationToken);
			}
		}

		/// <summary>
		/// Deletes a range of entities. Optionally, marks them as deleted without removing them permanently.
		/// </summary>
		/// <param name="entities">The entities to delete.</param>
		/// <param name="permanent">Specifies if the entities should be removed permanently.</param>
		public void DeleteRange(ICollection<TEntity> entities, bool permanent = false)
		{
			if (permanent)
			{
				_context.Set<TEntity>().RemoveRange(entities);
			}
			else
			{
				foreach (TEntity entity in entities)
				{
					entity.IsDeleted = true;
					entity.DeletedDate = DateTime.Now;
				}
				UpdateRange(entities);
			}
		}

		/// <summary>
		/// Asynchronously deletes a range of entities. Optionally, marks them as deleted without removing them permanently.
		/// </summary>
		/// <param name="entities">The entities to delete.</param>
		/// <param name="permanent">Specifies if the entities should be removed permanently.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The task representing the asynchronous operation.</returns>
		public async Task DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false, CancellationToken cancellationToken = default)
		{
			if (permanent)
			{
				_context.Set<TEntity>().RemoveRange(entities);
			}
			else
			{
				foreach (TEntity entity in entities)
				{
					entity.IsDeleted = true;
					entity.DeletedDate = DateTime.Now;
				}
				await UpdateRangeAsync(entities, cancellationToken);
			}
		}

		/// <summary>
		/// Retrieves a single entity matching the specified predicate.
		/// </summary>
		/// <param name="predicate">The predicate to filter entities.</param>
		/// <param name="enableTracking">Specifies if the entities should be tracked.</param>
		/// <param name="isDeleted">Specifies if the entities are deleted.</param>
		/// <param name="includeList">The related entities to include in the result.</param>
		/// <returns>The entity that matches the predicate, or null if no match is found.</returns>
		public TEntity? Get(Expression<Func<TEntity, bool>> predicate, bool enableTracking = true, bool? isDeleted = null, params string[] includeList)
		{
			IQueryable<TEntity> queryable = enableTracking ? _context.Set<TEntity>() : _context.Set<TEntity>().AsNoTracking();
			queryable = isDeleted != null ? queryable.Where(x => x.IsDeleted == isDeleted) : queryable;
			if (includeList != null)
			{
				queryable = includeList.Aggregate(queryable, (current, include) => current.Include(include));
			}
			return queryable.SingleOrDefault(predicate);
		}

		/// <summary>
		/// Asynchronously retrieves a single entity matching the specified predicate.
		/// </summary>
		/// <param name="predicate">The predicate to filter entities.</param>
		/// <param name="enableTracking">Specifies if the entities should be tracked.</param>
		/// <param name="isDeleted">Specifies if the entities are deleted.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <param name="includeList">The related entities to include in the result.</param>
		/// <returns>The task representing the asynchronous operation, with the entity that matches the predicate as the result.</returns>
		public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default, params string[] includeList)
		{
			IQueryable<TEntity> queryable = enableTracking ? _context.Set<TEntity>() : _context.Set<TEntity>().AsNoTracking();
			queryable = isDeleted != null ? queryable.Where(x => x.IsDeleted == isDeleted) : queryable;
			if (includeList != null)
			{
				queryable = includeList.Aggregate(queryable, (current, include) => current.Include(include));
			}
			return await queryable.SingleOrDefaultAsync(predicate, cancellationToken);
		}

		/// <summary>
		/// Retrieves an entity by its identifier.
		/// </summary>
		/// <param name="id">The identifier of the entity.</param>
		/// <param name="enableTracking">Specifies if the entity should be tracked.</param>
		/// <param name="isDeleted">Specifies if the entity is deleted.</param>
		/// <param name="includeList">The related entities to include in the result.</param>
		/// <returns>The entity with the specified identifier, or null if not found.</returns>
		public TEntity? GetById(TEntityId id, bool enableTracking = true, bool? isDeleted = null, params string[] includeList)
		{
			return Get(prd => prd.Id.Equals(id), enableTracking, isDeleted, includeList);
		}

		/// <summary>
		/// Asynchronously retrieves an entity by its identifier.
		/// </summary>
		/// <param name="id">The identifier of the entity.</param>
		/// <param name="enableTracking">Specifies if the entity should be tracked.</param>
		/// <param name="isDeleted">Specifies if the entity is deleted.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <param name="includeList">The related entities to include in the result.</param>
		/// <returns>The task representing the asynchronous operation, with the entity with the specified identifier as the result.</returns>
		public async Task<TEntity?> GetByIdAsync(TEntityId id, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default, params string[] includeList)
		{
			return await GetAsync(predicate: prd => prd.Id.Equals(id), enableTracking, isDeleted, cancellationToken, includeList);
		}

		/// <summary>
		/// Retrieves a queryable collection of entities with optional filtering and including related entities.
		/// </summary>
		/// <param name="predicate">The predicate to filter entities.</param>
		/// <param name="enableTracking">Specifies if the entities should be tracked.</param>
		/// <param name="isDeleted">Specifies if the entities are deleted.</param>
		/// <param name="includeList">The related entities to include in the result.</param>
		/// <returns>A queryable collection of entities.</returns>
		public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null, params string[] includeList)
		{
			IQueryable<TEntity> queryable = enableTracking ? _context.Set<TEntity>() : _context.Set<TEntity>().AsNoTracking();
			queryable = isDeleted.HasValue ? queryable.Where(prd => prd.IsDeleted == isDeleted) : queryable;
			if (includeList != null)
			{
				queryable = includeList.Aggregate(queryable, (current, include) => current.Include(include));
			}
			return predicate != null ? queryable.Where(predicate) : queryable;
		}

		/// <summary>
		/// Updates the specified entity in the context.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		public void Update(TEntity entity)
		{
			var tracking = _context.Set<TEntity>().Find(entity.Id);
			if (tracking != null)
			{
				_context.Entry(tracking).State = EntityState.Detached;
				entity.UpdatedDate = DateTime.Now;
				_context.Set<TEntity>().Update(entity);
			}
		}

		/// <summary>
		/// Asynchronously updates the specified entity in the context.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The task representing the asynchronous operation.</returns>
		public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			var tracking = await _context.Set<TEntity>().FindAsync(entity.Id);
			if (tracking != null)
			{
				_context.Entry(tracking).State = EntityState.Detached;
				entity.UpdatedDate = DateTime.Now;
				_context.Set<TEntity>().Update(entity);
			}
		}

		/// <summary>
		/// Updates a range of entities in the context.
		/// </summary>
		/// <param name="entities">The entities to update.</param>
		public void UpdateRange(ICollection<TEntity> entities)
		{
			foreach (TEntity entity in entities)
			{
				var tracking = _context.Set<TEntity>().Find(entity.Id);
				if (tracking != null)
				{
					_context.Entry(tracking).State = EntityState.Detached;
					entity.UpdatedDate = DateTime.Now;
				}
			}
			_context.Set<TEntity>().UpdateRange(entities);
		}

		/// <summary>
		/// Asynchronously updates a range of entities in the context.
		/// </summary>
		/// <param name="entities">The entities to update.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The task representing the asynchronous operation.</returns>
		public async Task UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
		{
			foreach (TEntity entity in entities)
			{
				var tracking = await _context.Set<TEntity>().FindAsync(entity.Id);
				if (tracking != null)
				{
					_context.Entry(tracking).State = EntityState.Detached;
					entity.UpdatedDate = DateTime.Now;
				}
			}
			_context.Set<TEntity>().UpdateRange(entities);
		}
	}
}
