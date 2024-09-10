using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Utilities.IoC.Interfaces
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
