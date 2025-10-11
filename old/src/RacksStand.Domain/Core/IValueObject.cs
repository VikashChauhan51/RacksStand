namespace RacksStand.Domain.Core;
public interface IValueObject<out T>
{
    T Value { get; }
}
