namespace RacksStand.Api.Core.Exceptions;

using RacksStand.Api.Core.Exceptions.Core;
using System.Net;
public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message)
        : base(message,
               (int)HttpStatusCode.NotFound,
               "Resource Not Found",
               "https://httpstatuses.com/404")
    { }
}

