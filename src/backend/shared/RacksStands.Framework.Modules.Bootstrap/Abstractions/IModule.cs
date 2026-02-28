using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;

namespace RacksStands.Framework.Modules.Bootstrap.Abstractions;

public interface IModule
{
    string Name => GetType().Name;
    int Order => 0;
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    void MapEndpoints(IEndpointRouteBuilder routes);
}
