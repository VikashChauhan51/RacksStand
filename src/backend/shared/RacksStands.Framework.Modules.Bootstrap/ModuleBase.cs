using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RacksStands.Framework.Modules.Bootstrap.Abstractions;

namespace RacksStands.Framework.Modules.Bootstrap;

public abstract class ModuleBase : IModule
{
    public virtual string Name => GetType().Name;

    public virtual int Order => 0;
    public abstract void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    public virtual void MapEndpoints(IEndpointRouteBuilder routes)
    {
    }
}
