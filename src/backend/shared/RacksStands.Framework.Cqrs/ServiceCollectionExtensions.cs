using Microsoft.Extensions.DependencyInjection;

namespace RacksStands.Framework.Cqrs;

public static class CqrsServiceCollectionExtensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddScoped<ICqrsDispatcher, CqrsDispatcher>();
        return services;
    }
}
