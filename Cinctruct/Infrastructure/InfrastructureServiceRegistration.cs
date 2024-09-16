using Infrastructure.Utilities.IoC;
using Infrastructure.Utilities.IoC.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
	/// <summary>
	/// A static class to register infrastructure services and resolve dependencies.
	/// </summary>
	public static class InfrastructureServiceRegistration
	{
		/// <summary>
		/// Adds dependency resolvers by loading the provided core modules into the service collection.
		/// </summary>
		/// <param name="services">The IServiceCollection to which dependencies will be added.</param>
		/// <param name="modules">An array of ICoreModule instances to load into the service collection.</param>
		/// <returns>The updated IServiceCollection with loaded modules.</returns>
		public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
		{
			foreach (var module in modules)
			{
				module.Load(services);
			}
			return ServiceTool.Create(services);
		}
	}
}
