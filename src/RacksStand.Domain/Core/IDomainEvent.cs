
namespace RacksStand.Domain.Core;

public interface IDomainEvent
{
    string Id { get; }
    DateTime OccurredOn { get; }
}
