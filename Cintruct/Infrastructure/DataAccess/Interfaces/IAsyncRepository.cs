using Infrastructure.Model.Entities.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncRepository<TEntity, TEntityId> where TEntity : IEntity<TEntityId>, new()
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default);
        Task<long> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool? isDeleted = null, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, bool permanent = false, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default, params string[] includeList);
        Task<TEntity?> GetByIdAsync(TEntityId id, bool enableTracking = true, bool? isDeleted = null, CancellationToken cancellationToken = default, params string[] includeList);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
