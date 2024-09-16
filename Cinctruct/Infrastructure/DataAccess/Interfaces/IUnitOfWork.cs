using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Interfaces
{
	/// <summary>
	/// Defines the contract for a unit of work that operates on a specific DbContext.
	/// </summary>
	/// <typeparam name="TContext">The type of the DbContext.s</typeparam>
	public interface IUnitOfWork<TContext> where TContext : DbContext
	{
		/// <summary>
		/// Commits the current transaction asynchronously.
		/// </summary>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task CommitAsync();

		/// <summary>
		/// Commits the current transaction synchronously.
		/// </summary>
		void Commit();
	}
}
