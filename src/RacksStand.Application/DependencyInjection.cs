using FluentValidation;
using MediatorForge.CQRS.Authorizers;
using MediatorForge.CQRS.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using RacksStand.Application.Core;
using RacksStand.Application.Core.Behaviors;

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
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            config.AddOpenBehavior(typeof(FluentValidationBehavior<,>));
        });

        // Register request authorizer
        services.AddTransient(typeof(IAuthorizer<>), typeof(RequestAuthorizer<>));
        //register validators
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}
