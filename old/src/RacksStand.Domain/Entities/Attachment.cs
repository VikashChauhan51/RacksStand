using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class Attachment : Entity<AttachmentId>
{
    public required string ContentType { get; set; }
    public required string Extension { get; set; }
    public required string Name { get; set; }
    public string? Url { get; set; }
}
