using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;

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
