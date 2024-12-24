using FluentValidation;
using MediatorForge.Adapters;
using MediatorForge.CQRS.Authorizers;
using MediatorForge.CQRS.Behaviors;
using MediatorForge.CQRS.Validators;
using Microsoft.Extensions.DependencyInjection;
using RacksStand.Application.Features.Categories.CreateCategory;

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
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // Register default authorizer and validator
        services.AddTransient(typeof(IAuthorizer<>), typeof(DefaultAuthorizer<>));
        //register validators
        services.AddValidatorsFromAssembly(assembly);
        // Add FluentValidatorAdapter
        services.AddMediatorForgeFluentValidatorAdapter();
        return services;
    }
}
