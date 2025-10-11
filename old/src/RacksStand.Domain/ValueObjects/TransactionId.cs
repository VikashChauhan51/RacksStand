using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record TransactionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public TransactionId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("TransactionId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static TransactionId Of(string value) => new(value);
    public static implicit operator TransactionId(string value) => Of(value);
    public static explicit operator string(TransactionId transactionId) => transactionId.Value;
}
