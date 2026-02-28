using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using RacksStands.Framework.Modules.Bootstrap.Abstractions;
using System;
using System.Linq;

namespace RacksStands.Framework.Modules.Bootstrap.Extensions;

public static class ModuleApplicationBuilderExtensions
{

    public static IEndpointRouteBuilder UseModules(this IEndpointRouteBuilder routes)
    {   
        ArgumentNullException.ThrowIfNull(routes, nameof(routes));

        var modules =  routes.ServiceProvider.GetServices<IModule>()
            .OrderBy(m => m.Order)
            .ThenBy(m => m.Name)
            .ToList();

        foreach (var module in modules)
        {
            module.MapEndpoints(routes);
        }
        return routes;
    }
}
