using FluentValidation;

namespace RacksStands.Framework.Caching;

public class CacheOptions
{
    public string? RedisConnectionString { get; set; }
    public bool UseHybridCache { get; set; }
}

public class CacheOptionsValidator : AbstractValidator<CacheOptions>
{
    public CacheOptionsValidator()
    {
        RuleFor(x => x.RedisConnectionString)
            .NotEmpty().When(x => x.UseHybridCache)
            .WithMessage("Redis connection string is required for hybrid cache.");
    }
}
