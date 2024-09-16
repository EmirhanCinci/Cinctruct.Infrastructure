using Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Implementations.EntityFrameworkCore
{
	/// <summary>
	/// Represents a unit of work for managing database transactions with a specific DbContext.
	/// </summary>
	/// <typeparam name="TContext">The type of the DbContext.</typeparam>
	public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
	{
		private readonly TContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork{TContext}"/> class.
		/// </summary>
		/// <param name="context">The DbContext to use for the unit of work.</param>
		public UnitOfWork(TContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Commits all changes made in the context to the database.
		/// </summary>
		public void Commit()
		{
			_context.SaveChanges();
		}

		/// <summary>
		/// Asynchronously commits all changes made in the context to the database.
		/// </summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
