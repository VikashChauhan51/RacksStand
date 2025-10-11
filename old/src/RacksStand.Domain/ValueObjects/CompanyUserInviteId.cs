using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CompanyUserInviteId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public CompanyUserInviteId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanyUserInviteId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static CompanyUserInviteId Of(string value) => new(value);
    public static implicit operator CompanyUserInviteId(string value) => Of(value);
    public static explicit operator string(CompanyUserInviteId companyUserInviteId) => companyUserInviteId.Value;
}
