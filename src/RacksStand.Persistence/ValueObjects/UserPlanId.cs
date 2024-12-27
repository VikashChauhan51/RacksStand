using RacksStand.Persistence.Exceptions;

namespace RacksStand.Persistence.ValueObjects;

public record UserPlanId
{
    public string Value { get; }
    private UserPlanId(string value) => Value = value;
    public static UserPlanId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserPlanId cannot be null or whitespace.");
        }

        return new UserPlanId(value);
    }
}
