using Microsoft.Extensions.DependencyInjection;
using Polly;
using FluentValidation;

namespace RacksStands.Framework.Resiliency;

public static class ResiliencyServiceCollectionExtensions
{
    public static IServiceCollection AddRacksStandsResiliency(this IServiceCollection services, Action<ResiliencyOptions> configure)
    {
        var options = new ResiliencyOptions();
        configure(options);
        var validator = new ResiliencyOptionsValidator();
        var result = validator.Validate(options);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        services.AddSingleton<IAsyncPolicy>(Policy.WrapAsync(
            Policy.Handle<Exception>().WaitAndRetryAsync(options.RetryCount, retry => TimeSpan.FromSeconds(2)),
            Policy.Handle<Exception>().CircuitBreakerAsync(options.CircuitBreakerFailures, options.CircuitBreakerDuration)
        ));
        return services;
    }
}
