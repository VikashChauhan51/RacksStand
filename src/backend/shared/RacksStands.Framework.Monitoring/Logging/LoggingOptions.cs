using FluentValidation;

namespace RacksStands.Framework.Monitoring.Logging;

public class LoggingOptions
{
    public string? LogLevel { get; set; }
    public string? OutputTemplate { get; set; }
}

public class LoggingOptionsValidator : AbstractValidator<LoggingOptions>
{
    public LoggingOptionsValidator()
    {
        RuleFor(x => x.LogLevel).NotEmpty();
        RuleFor(x => x.OutputTemplate).NotEmpty();
    }
}
