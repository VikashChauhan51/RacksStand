using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace RacksStands.Framework.Modules.Base;

public interface IModule
{
    void ConfigureServices(IServiceCollection services);
    void ConfigureApplication(WebApplication app);
}
