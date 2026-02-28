using Microsoft.Extensions.DependencyInjection;
using Serilog;
using FluentValidation;

namespace RacksStands.Framework.Monitoring.Logging;

public static class LoggingExtensions
{
    public static IServiceCollection AddRacksStandsLogging(this IServiceCollection services, Action<LoggingOptions> configure)
    {
        var options = new LoggingOptions();
        configure(options);
        var validator = new LoggingOptionsValidator();
        var result = validator.Validate(options);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(outputTemplate: options.OutputTemplate)
            .CreateLogger();

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
        return services;
    }
}
