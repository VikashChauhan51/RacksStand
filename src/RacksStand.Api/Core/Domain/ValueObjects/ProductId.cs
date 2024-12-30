using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;
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
