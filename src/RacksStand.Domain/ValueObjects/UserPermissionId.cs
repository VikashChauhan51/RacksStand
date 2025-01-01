using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record UserPermissionId
{
    public string Value { get; }
    private UserPermissionId(string value) => Value = value;
    public static UserPermissionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserPermissionId cannot be null or whitespace.");
        }

        return new UserPermissionId(value);
    }
}
