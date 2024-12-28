namespace RacksStand.Persistence.Exceptions;
public class DuplicateEntityException : DomainException
{
    public DuplicateEntityException() : base("Duplicate entity exists.") { }
    public DuplicateEntityException(string message) : base(message) { }
    public DuplicateEntityException(string message, Exception innerException) : base(message, innerException) { }
}
