using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PlanId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public PlanId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PlanId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static PlanId Of(string value) => new(value);
    public static implicit operator PlanId(string value) => Of(value);
    public static explicit operator string(PlanId planId) => planId.Value;
}
