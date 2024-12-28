using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;

public record TransactionId
{
    public string Value { get; }
    private TransactionId(string value) => Value = value;
    public static TransactionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("TransactionId cannot be null or whitespace.");
        }

        return new TransactionId(value);
    }
}
