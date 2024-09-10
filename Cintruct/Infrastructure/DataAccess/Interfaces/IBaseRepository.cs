using Infrastructure.Model.Entities.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface IBaseRepository<TEntity, TEntityId> : IAsyncRepository<TEntity, TEntityId>, IRepository<TEntity, TEntityId> where TEntity : IEntity<TEntityId>, new()
    {
        IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true, bool? isDeleted = null, params string[] includeList);
    }
}
