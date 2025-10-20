namespace RacksStand.Api.Core.Exceptions;

using RacksStand.Api.Core.Exceptions.Core;
using System.Net;
public class TooManyRequestsException : ExceptionBase
{
    public TooManyRequestsException(string message = "Too many requests.")
        : base(message,
               (int)HttpStatusCode.TooManyRequests,
               "Too Many Requests",
               "https://httpstatuses.com/429")
    { }
}


