using Infrastructure.Utilities.IoC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Infrastructure.Utilities.IoC.Implementations
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<Stopwatch>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
