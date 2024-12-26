using RacksStand.Persistence.Exceptions;


namespace RacksStand.Persistence.ValueObjects;
public record InviteId
{
    public string Value { get; }
    private InviteId(string value) => Value = value;
    public static InviteId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("InviteId cannot be null or whitespace.");
        }

        return new InviteId(value);
    }
}
