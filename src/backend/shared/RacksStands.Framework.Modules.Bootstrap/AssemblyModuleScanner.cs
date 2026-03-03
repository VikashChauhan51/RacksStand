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
            .Where(t => IsModuleClass(t))
            .Select(t => (IModule)Activator.CreateInstance(t))
            .OrderBy(m => m.Order)
            .ThenBy(m => m.Name)
            .ToList();
    }

    public static IReadOnlyList<Type> GetLoadableTypes(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

        return assembly.GetTypes()
            .Where(t => IsModuleClass(t))
            .ToList();
    }


    private static bool IsModuleClass(Type t)
    {
        return t.IsClass &&
                    typeof(IModule).IsAssignableFrom(t) &&
                    !t.IsInterface &&
                    !t.IsAbstract &&
                    t.GetConstructor(Type.EmptyTypes) is not null;
    }
}
