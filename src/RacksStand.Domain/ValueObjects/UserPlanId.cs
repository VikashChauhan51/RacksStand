using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record UserPlanId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private UserPlanId(string value) => _value = value;
    public static UserPlanId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserPlanId cannot be null or whitespace.");
        }

        return new UserPlanId(value);
    }
}
