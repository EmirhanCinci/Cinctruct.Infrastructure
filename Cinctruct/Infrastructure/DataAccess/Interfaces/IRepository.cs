using Infrastructure.Model.Entities.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface IRepository<TEntity, TEntityId> where TEntity : IEntity<TEntityId>, new()
    {
        TEntity Add(TEntity entity);
        ICollection<TEntity> AddRange(ICollection<TEntity> entities);
        bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null);
        long Count(Expression<Func<TEntity, bool>>? predicate = null, bool? isDeleted = null);
        void Delete(TEntity entity, bool permanent = false);
        void DeleteRange(ICollection<TEntity> entities, bool permanent = false);
        TEntity? Get(Expression<Func<TEntity, bool>> predicate, bool enableTracking = true, bool? isDeleted = null, params string[] includeList);
        TEntity? GetById(TEntityId id, bool enableTracking = true, bool? isDeleted = null, params string[] includeList);
        void Update(TEntity entity);
        void UpdateRange(ICollection<TEntity> entities);
    }
}
