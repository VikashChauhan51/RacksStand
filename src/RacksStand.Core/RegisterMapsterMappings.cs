using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RacksStand.Core;
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
