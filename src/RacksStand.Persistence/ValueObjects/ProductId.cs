using RacksStand.Persistence.Exceptions;

namespace RacksStand.Persistence.ValueObjects;
public record ProductId
{
    public string Value { get; }
    private ProductId(string value) => Value = value;
    public static ProductId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductId cannot be null or whitespace.");
        }

        return new ProductId(value);
    }
}
