using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RacksStand.Core;
public static class RegisterFeatureModules
{
    public static void RegisterModules(IServiceCollection services,IConfiguration configuration, Assembly assembly)
    {
        var featureModules = assembly
            .GetTypes()
            .Where(t =>
            typeof(IFeatureModule).IsAssignableFrom(t) &&
            !t.IsInterface &&
            !t.IsAbstract)
            .ToList();

        foreach (var moduleType in featureModules)
        {
            var moduleInstance = Activator.CreateInstance(moduleType) as IFeatureModule;
            moduleInstance?.RegisterServices(services, configuration, assembly);
        }
    }
}
