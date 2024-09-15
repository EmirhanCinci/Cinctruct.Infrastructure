using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        Task CommitAsync();
        void Commit();
    }
}
