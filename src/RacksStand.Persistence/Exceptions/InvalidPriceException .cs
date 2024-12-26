namespace RacksStand.Persistence.Exceptions;
public class InvalidPriceException : DomainException
{
    public InvalidPriceException(string message) : base(message) { }
    public InvalidPriceException(string message, Exception innerException) : base(message, innerException) { }
}
