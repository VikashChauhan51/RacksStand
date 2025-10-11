namespace RacksStand.Domain.Exceptions;
public class EntityNotFoundException: DomainException
{
    public EntityNotFoundException() : base("Entity not found.") { }
    public EntityNotFoundException(string message) : base(message) { }
    public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}
