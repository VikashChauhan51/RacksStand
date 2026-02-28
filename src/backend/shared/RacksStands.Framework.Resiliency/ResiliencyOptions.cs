using FluentValidation;

namespace RacksStands.Framework.Resiliency;

public class ResiliencyOptions
{
    public int RetryCount { get; set; } = 3;
    public int CircuitBreakerFailures { get; set; } = 5;
    public TimeSpan CircuitBreakerDuration { get; set; } = TimeSpan.FromSeconds(30);
}

public class ResiliencyOptionsValidator : AbstractValidator<ResiliencyOptions>
{
    public ResiliencyOptionsValidator()
    {
        RuleFor(x => x.RetryCount).GreaterThan(0);
        RuleFor(x => x.CircuitBreakerFailures).GreaterThan(0);
        RuleFor(x => x.CircuitBreakerDuration).GreaterThan(TimeSpan.Zero);
    }
}
