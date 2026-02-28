using RacksStands.Framework.Modules.Bootstrap.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RacksStands.Framework.Modules.Bootstrap;

public static class AssemblyModuleScanner
{
    public static IReadOnlyList<IModule> Discover(IEnumerable<Assembly> assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies, nameof(assemblies));

        return assemblies.SelectMany(a => a.GetTypes())
            .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .OrderBy(m => m.Order)
            .ThenBy(m => m.Name)
            .ToList();
    }
}
