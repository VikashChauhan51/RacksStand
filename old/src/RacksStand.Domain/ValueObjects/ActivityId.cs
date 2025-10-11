using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ActivityId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public ActivityId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ActivityId cannot be null or whitespace.");
        }

        _value = value;
    }

    public static ActivityId Of(string value) => new(value);
    public static implicit operator ActivityId(string value) => Of(value);
    public static explicit operator string(ActivityId activityId) => activityId.Value;
}

