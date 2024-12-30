using System.Reflection;

namespace RacksStand.Api.Core.Infrastructure;

public static class MapsterConfig
{
    public static void RegisterMappings(Assembly assembly)
    {
        var mappingConfigurations = assembly
            .GetTypes()
            .Where(t => typeof(IMappingConfiguration).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var configType in mappingConfigurations)
        {
            var configInstance = Activator.CreateInstance(configType) as IMappingConfiguration;
            configInstance?.Configure();
        }
    }
}

