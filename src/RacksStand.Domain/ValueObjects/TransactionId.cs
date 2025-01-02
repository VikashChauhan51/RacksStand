using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record TransactionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private TransactionId(string value) => _value = value;
    public static TransactionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("TransactionId cannot be null or whitespace.");
        }

        return new TransactionId(value);
    }
}
