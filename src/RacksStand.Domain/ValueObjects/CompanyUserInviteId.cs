using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CompanyUserInviteId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private CompanyUserInviteId(string value) => _value = value;
    public static CompanyUserInviteId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanyUserInviteId cannot be null or whitespace.");
        }

        return new CompanyUserInviteId(value);
    }
}
