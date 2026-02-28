using Microsoft.Extensions.DependencyInjection;
using RacksStands.Framework.Cqrs.Abstractions;
using RacksStands.Framework.Cqrs.Behaviors;

namespace RacksStands.Framework.Cqrs.Extensions;

public static class CqrsServiceCollectionExtensions
{

    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }
}
