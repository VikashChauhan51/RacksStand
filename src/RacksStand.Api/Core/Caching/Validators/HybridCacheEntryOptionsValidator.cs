namespace RacksStand.Api.Core.Caching.Validators;

using Microsoft.Extensions.Caching.Hybrid;

public sealed class HybridCacheEntryOptionsValidator : AbstractValidator<HybridCacheEntryOptions>
{
    public HybridCacheEntryOptionsValidator()
    {
        RuleFor(x => x.Expiration)
            .NotNull()
            .WithMessage("Cache 'Expiration' must be configured.");

        RuleFor(x => x.LocalCacheExpiration)
            .NotNull()
            .WithMessage("Cache 'LocalCacheExpiration' must be configured.")
            .LessThanOrEqualTo(x => x.Expiration ?? TimeSpan.FromMinutes(Constants.DefaultCacheExpirationMinutes))
            .WithMessage("'LocalCacheExpiration' cannot exceed the overall 'Expiration'.");
    }
}
