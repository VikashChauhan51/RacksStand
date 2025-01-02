using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PlanId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private PlanId(string value) => _value = value;
    public static PlanId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PlanId cannot be null or whitespace.");
        }

        return new PlanId(value);
    }
}
