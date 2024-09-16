using Infrastructure.Utilities.IoC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Infrastructure.Utilities.IoC.Implementations
{
	/// <summary>
	/// Implements the <see cref="ICoreModule"/> interface to configure and add core services to the service collection.
	/// </summary>
	public class CoreModule : ICoreModule
	{
		/// <summary>
		/// Configures and adds core services to the provided service collection.
		/// </summary>
		/// <param name="services">The collection of services to which the module should add services.</param>
		public void Load(IServiceCollection services)
		{
			services.AddSingleton<Stopwatch>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}
	}
}
