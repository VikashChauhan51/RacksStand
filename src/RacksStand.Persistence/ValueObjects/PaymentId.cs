using RacksStand.Persistence.Exceptions;

namespace RacksStand.Persistence.ValueObjects;

public record PaymentId
{
    public string Value { get; }
    private PaymentId(string value) => Value = value;
    public static PaymentId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PaymentId cannot be null or whitespace.");
        }

        return new PaymentId(value);
    }
}
