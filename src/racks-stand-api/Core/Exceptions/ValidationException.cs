namespace RacksStand.Api.Core.Exceptions;

using RacksStand.Api.Core.Exceptions.Core;
using System.Net;

public class ValidationException : ExceptionBase
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors)
        : base("Validation failed for one or more fields.",
               (int)HttpStatusCode.BadRequest,
               "Validation Error",
               "https://httpstatuses.com/400")
    {
        Errors = errors;
    }

    public ValidationException(string message)
        : base(message,
               (int)HttpStatusCode.BadRequest,
               "Validation Error",
               "https://httpstatuses.com/400")
    {
        Errors = new();
    }
}


