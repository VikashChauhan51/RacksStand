using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RacksStands.Framework.Modules.Bootstrap.Extensions;

public static class ModuleServiceCollectionExtensions
{
    public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration,
        IEnumerable<Assembly> assemblies)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        var modules  = AssemblyModuleScanner.Discover(assemblies);

        foreach (var module in modules)
        {
            module.ConfigureServices(services, configuration);
        }
        return services;
    }
}
