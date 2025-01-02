using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record UserPermissionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private UserPermissionId(string value) => _value = value;
    public static UserPermissionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserPermissionId cannot be null or whitespace.");
        }

        return new UserPermissionId(value);
    }
}
