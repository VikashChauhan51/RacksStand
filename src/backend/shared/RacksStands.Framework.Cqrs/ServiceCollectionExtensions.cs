using Microsoft.Extensions.DependencyInjection;
using RacksStands.Framework.Cqrs.Abstractions;

namespace RacksStands.Framework.Cqrs;

public static class CqrsServiceCollectionExtensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();
        return services;
    }
}
