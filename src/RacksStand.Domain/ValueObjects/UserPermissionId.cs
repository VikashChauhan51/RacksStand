using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record UserPermissionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public UserPermissionId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserPermissionId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static UserPermissionId Of(string value) => new(value);
    public static implicit operator UserPermissionId(string value) => Of(value);
    public static explicit operator string(UserPermissionId permissionId) => permissionId.Value;
}
