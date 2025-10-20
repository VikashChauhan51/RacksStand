namespace RacksStand.Api.Core.Exceptions;

using RacksStand.Api.Core.Exceptions.Core;
using System.Net;

public class ConflictException : ExceptionBase
{
    public ConflictException(string message)
        : base(message,
               (int)HttpStatusCode.Conflict,
               "Request Conflict",
               "https://httpstatuses.com/409")
    { }
}


