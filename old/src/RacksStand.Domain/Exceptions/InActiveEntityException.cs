namespace RacksStand.Domain.Exceptions;

public class InActiveEntityException : DomainException
{
    public InActiveEntityException() : base("Entity is in inactive state.") { }
    public InActiveEntityException(string message) : base(message) { }
    public InActiveEntityException(string message, Exception innerException) : base(message, innerException) { }
}
