using Microsoft.AspNetCore.Routing;

namespace RacksStands.Framework.Modules.Bootstrap.Abstractions;

public interface IEndpoint
{
    void Map(RouteGroupBuilder group);
}
