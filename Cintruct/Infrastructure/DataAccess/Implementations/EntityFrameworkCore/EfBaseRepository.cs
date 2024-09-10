using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Model.Entities.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Implementations.EntityFrameworkCore
{
    public abstract class EfBaseRepository<TEntity, TEntityId, TContext> : IBaseRepository<TEntity, TEntityId> where TEntity : BaseEntity<TEntityId>, new() where TContext : DbContext
    {
        protected TContext _context;
        protected EfBaseRepository(TContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.CreatedDate = DateTime.Now;
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            return entity;
        }

        public ICollection<TEntity> AddRange(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
            }
            _context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (TEntity entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
            }
            await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null)
        {
            IQueryable<TEntity> queryable = !enableTracking ? _context.Set<TEntity>().AsNoTracking() : _context.Set<TEntity>();
            queryable = isDeleted.HasValue ? queryable.Where(prd => prd.IsDeleted == isDeleted) : queryable;
            return predicate != null ? queryable.Where(predicate).Any() : queryable.Any();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = !enableTracking ? _context.Set<TEntity>().AsNoTracking() : _context.Set<TEntity>();
            queryable = isDeleted.HasValue ? queryable.Where(prd => prd.IsDeleted == isDeleted) : queryable;
            return predicate != null ? await queryable.Where(predicate).AnyAsync(cancellationToken) : await queryable.AnyAsync(cancellationToken);
        }

        public long Count(Expression<Func<TEntity, bool>>? predicate = null, bool? isDeleted = null)
        {
            return _context.Set<TEntity>().Where(entity => !isDeleted.HasValue || entity.IsDeleted == isDeleted.Value).Count(predicate ?? (entity => true));
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool? isDeleted = null, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().Where(entity => !isDeleted.HasValue || entity.IsDeleted == isDeleted.Value).CountAsync(predicate ?? (entity => true), cancellationToken);
        }

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

        public TEntity? GetById(TEntityId id, bool enableTracking = true, bool? isDeleted = null, params string[] includeList)
        {
            return Get(prd => prd.Id.Equals(id), enableTracking, isDeleted, includeList);
        }

        public async Task<TEntity?> GetByIdAsync(TEntityId id, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default, params string[] includeList)
        {
            return await GetAsync(predicate: prd => prd.Id.Equals(id), enableTracking, isDeleted, cancellationToken, includeList);
        }

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
