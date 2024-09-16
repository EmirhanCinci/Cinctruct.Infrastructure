using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Utilities.IoC.Interfaces
{
	/// <summary>
	/// Defines the contract for modules that configure and add services to the service collection.
	/// </summary>
	public interface ICoreModule
	{
		/// <summary>
		/// Configures and adds services to the provided service collection.
		/// </summary>
		/// <param name="services">The collection of services to which the module should add services.</param>
		void Load(IServiceCollection services);
	}
}
