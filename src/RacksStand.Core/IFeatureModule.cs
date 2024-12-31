using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace RacksStand.Core;
public interface IFeatureModule
{
    void RegisterServices(IServiceCollection services, IConfiguration configuration, Assembly assembly);
}
