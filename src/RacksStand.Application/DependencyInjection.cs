using FluentValidation;
using MediatorForge.CQRS.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace RacksStand.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplcationServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        // register handlers
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        //register validators
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}
