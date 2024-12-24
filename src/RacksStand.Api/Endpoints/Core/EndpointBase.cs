using Carter;
using Microsoft.AspNetCore.Mvc;

namespace RacksStand.Api.Endpoints.Core;

public abstract class EndpointBase : CarterModule
{
    public abstract string RouteName { get; }

    protected IResult Problem(Exception error)
    {
        return error switch
        {
            MediatorForge.CQRS.Exceptions.ValidationException validationException =>
            Results.ValidationProblem(validationException.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                g =>
                g.Select(e => e.ErrorMessage)
                .ToArray()
                ), validationException.Message),

            FluentValidation.ValidationException validationException =>
            Results.ValidationProblem(validationException.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                g =>
                g.Select(e => e.ErrorMessage)
                .ToArray()
                ), validationException.Message),

            MediatorForge.CQRS.Exceptions.AuthorizationException => Results.Forbid(),
            UnauthorizedAccessException => Results.Challenge(),
            _ => Results.BadRequest(error)
        };
    }
}
