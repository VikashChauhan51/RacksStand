using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CategoryId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public CategoryId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CategoryId cannot be null or whitespace.");
        }

        _value = value;
    }
    public static CategoryId Of(string value)=> new(value);

    public static implicit operator CategoryId(string value) => Of(value);
    public static explicit operator string(CategoryId categoryId) => categoryId.Value;
}
