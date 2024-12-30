using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;
public record SupplierId
{
    public string Value { get; }
    private SupplierId(string value) => Value = value;
    public static SupplierId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SupplierId cannot be null or whitespace.");
        }

        return new SupplierId(value);
    }
}
