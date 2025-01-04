using System.Reflection;

namespace RacksStand.Api.Core.Abstractions;

public interface IFeatureModule
{
    void RegisterServices(IServiceCollection services, IConfiguration configuration, Assembly assembly);
}


