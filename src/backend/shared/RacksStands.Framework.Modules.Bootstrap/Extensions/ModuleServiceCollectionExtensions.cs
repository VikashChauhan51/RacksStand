using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RacksStands.Framework.Modules.Bootstrap.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        var modules = AssemblyModuleScanner.Discover(assemblies);

        foreach (var module in modules)
        {
            module.ConfigureServices(services, configuration);
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IModule>(module));
        }
        return services;
    }

    public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration,
        params Type[] moduleTypes)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        ArgumentNullException.ThrowIfNull(moduleTypes, nameof(moduleTypes));

        var assemblies = moduleTypes.Select(t => t.Assembly).Distinct();
        var modules = AssemblyModuleScanner.Discover(assemblies);

        return services.AddModules(configuration, assemblies);
    }
}
