namespace RacksStand.Api.Core.Exceptions;

using RacksStand.Api.Core.Exceptions.Core;
using System.Net;

public class UnauthorizedException : ExceptionBase
{
    public UnauthorizedException(string message = "Unauthorized access.")
        : base(message,
               (int)HttpStatusCode.Unauthorized,
               "Unauthorized",
               "https://httpstatuses.com/401")
    { }
}


