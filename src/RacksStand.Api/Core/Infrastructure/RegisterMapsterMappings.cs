using RacksStand.Api.Core.Abstractions;
using System.Reflection;

namespace RacksStand.Api.Core.Infrastructure;

public static class RegisterMapsterMappings
{
    public static void Register(Assembly assembly)
    {
        var featureModules = assembly
            .GetTypes()
            .Where(t =>
            typeof(IMapsterConfig).IsAssignableFrom(t) &&
            !t.IsInterface &&
            !t.IsAbstract)
            .ToList();

        foreach (var moduleType in featureModules)
        {
            var moduleInstance = Activator.CreateInstance(moduleType) as IMapsterConfig;
            moduleInstance?.Configure();
        }
    }
}
