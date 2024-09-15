using Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Implementations.EntityFrameworkCore
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
