using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record UserPlanId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public UserPlanId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserPlanId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static UserPlanId Of(string value) => new(value);
    public static implicit operator UserPlanId(string value) => Of(value);
    public static explicit operator string(UserPlanId userPlanId) => userPlanId.Value;
}
