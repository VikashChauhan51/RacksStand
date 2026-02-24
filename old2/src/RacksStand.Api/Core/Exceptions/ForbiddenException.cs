namespace RacksStand.Api.Core.Exceptions;

using RacksStand.Api.Core.Exceptions.Core;
using System.Net;


public class ForbiddenException : ExceptionBase
{
    public ForbiddenException(string message = "Access denied.")
        : base(message,
               (int)HttpStatusCode.Forbidden,
               "Access Denied",
               "https://httpstatuses.com/403")
    { }
}

