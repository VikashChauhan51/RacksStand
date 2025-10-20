namespace RacksStand.Api.Core.Exceptions;

using RacksStand.Api.Core.Exceptions.Core;
using System.Net;

public class UnprocessableEntityException : ExceptionBase
{
    public UnprocessableEntityException(string message)
        : base(message,
               (int)HttpStatusCode.UnprocessableEntity,
               "Unprocessable Entity",
               "https://httpstatuses.com/422")
    { }
}

