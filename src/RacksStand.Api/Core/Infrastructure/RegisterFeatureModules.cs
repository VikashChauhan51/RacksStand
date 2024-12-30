using RacksStand.Api.Core.Abstractions;
using System.Reflection;

namespace RacksStand.Api.Core.Infrastructure;

public static class RegisterFeatureModules
{
    public static void RegisterModules(IServiceCollection services, Assembly assembly)
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
            moduleInstance?.RegisterServices(services);
        }
    }
}
