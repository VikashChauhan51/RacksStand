using System.Reflection;

namespace RacksStand.Api.Endpoints.Core;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var mappingConfigurations = Assembly.GetExecutingAssembly()
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

