using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record AttachmentId
{
    public string Value { get; }
    private AttachmentId(string value) => Value = value;
    public static AttachmentId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("AttachmentId cannot be null or whitespace.");
        }

        return new AttachmentId(value);
    }
}
