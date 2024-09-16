using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Utilities.IoC
{
	/// <summary>
	/// A utility class for managing and providing access to the service provider.s
	/// </summary>
	public class ServiceTool
	{
		/// <summary>
		/// Gets the service provider instance, which is used to resolve services.
		/// </summary>
		public static IServiceProvider ServiceProvider { get; private set; }

		/// <summary>
		/// Builds and sets the service provider from the given service collection.
		/// </summary>
		/// <param name="services">The collection of services to build the service provider from.</param>
		/// <returns>The input service collection.</returns>
		public static IServiceCollection Create(IServiceCollection services)
		{
			ServiceProvider = services.BuildServiceProvider();
			return services;
		}
	}
}
