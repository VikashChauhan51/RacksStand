using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CompanyUserInviteId
{
    public string Value { get; }
    private CompanyUserInviteId(string value) => Value = value;
    public static CompanyUserInviteId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanyUserInviteId cannot be null or whitespace.");
        }

        return new CompanyUserInviteId(value);
    }
}
