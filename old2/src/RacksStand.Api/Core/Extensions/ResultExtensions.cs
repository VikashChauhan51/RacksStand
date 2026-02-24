namespace RacksStand.Api.Core.Extensions;

using Microsoft.AspNetCore.Mvc;
using ResultifyCore;
public static class ResultExtensions
{

    public static IResult ToResult<T>(this Outcome<T> result, string? url = null)
    {
        var errorDictionary = ToErrorDictionary(result.Errors);

        return result.Status switch
        {
            ResultState.Success => Results.Ok(result.Value),
            ResultState.Created => Results.Created(url, result.Value),
            ResultState.NoContent => Results.NoContent(),

            ResultState.Conflict => Results.Conflict(new ProblemDetails
            {
                Title = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.Validation => Results.BadRequest(new ProblemDetails
            {
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.NotFound => Forbidden(errorDictionary),
            ResultState.Problem => Results.Problem(new ProblemDetails
            {
                Title = "A problem occurred",
                Status = StatusCodes.Status500InternalServerError,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.Unauthorized => UnAuthorized(errorDictionary),
            ResultState.Forbidden => Results.Forbid(),

            ResultState.Failure => Results.Problem(new ProblemDetails
            {
                Title = "Something went wrong.",
                Status = StatusCodes.Status500InternalServerError,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.CriticalError => Results.Problem(new ProblemDetails
            {
                Title = "Critical error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.Unavailable => Results.Problem(new ProblemDetails
            {
                Title = "Service unavailable.",
                Status = StatusCodes.Status503ServiceUnavailable,
                Extensions = { ["errors"] = errorDictionary }
            }),

            _ => Results.UnprocessableEntity(new ProblemDetails
            {
                Title = "Unprocessable entity.",
                Status = StatusCodes.Status422UnprocessableEntity,
                Extensions = { ["errors"] = errorDictionary }
            }),
        };
    }
    public static IResult ToResult(this Outcome result)
    {
        var errorDictionary = ToErrorDictionary(result.Errors);

        return result.Status switch
        {
            ResultState.Success => Results.Ok(),
            ResultState.Created => Results.Created(),
            ResultState.NoContent => Results.NoContent(),

            ResultState.Conflict => Results.Conflict(new ProblemDetails
            {
                Title = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.Validation => Results.BadRequest(new ProblemDetails
            {
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.NotFound => Forbidden(errorDictionary),
            ResultState.Problem => Results.Problem(new ProblemDetails
            {
                Title = "A problem occurred",
                Status = StatusCodes.Status500InternalServerError,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.Unauthorized => UnAuthorized(errorDictionary),
            ResultState.Forbidden => Results.Forbid(),

            ResultState.Failure => Results.Problem(new ProblemDetails
            {
                Title = "Something went wrong.",
                Status = StatusCodes.Status500InternalServerError,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.CriticalError => Results.Problem(new ProblemDetails
            {
                Title = "Critical error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Extensions = { ["errors"] = errorDictionary }
            }),

            ResultState.Unavailable => Results.Problem(new ProblemDetails
            {
                Title = "Service unavailable.",
                Status = StatusCodes.Status503ServiceUnavailable,
                Extensions = { ["errors"] = errorDictionary }
            }),

            _ => Results.UnprocessableEntity(new ProblemDetails
            {
                Title = "Unprocessable entity.",
                Status = StatusCodes.Status422UnprocessableEntity,
                Extensions = { ["errors"] = errorDictionary }
            }),
        };
    }

    private static IResult Forbidden(IDictionary<string, string[]> errors)
    {
        return Results.Problem(new ProblemDetails
        {
            Title = "Forbidden.",
            Status = StatusCodes.Status403Forbidden,
            Extensions = { ["errors"] = errors }
        });
    }

    private static IResult UnAuthorized(IDictionary<string, string[]> errors)
    {
        return Results.Problem(new ProblemDetails
        {
            Title = "Unauthorized.",
            Status = StatusCodes.Status401Unauthorized,
            Extensions = { ["errors"] = errors }
        });
    }

    private static Dictionary<string, string[]> ToErrorDictionary(IEnumerable<OutcomeError>? errors)
    {
        if (errors == null || !errors.Any())
            return new();

        return errors
            .GroupBy(e => string.IsNullOrWhiteSpace(e.Identifier) ? "General" : e.Identifier)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.Message).ToArray()
            );
    }
}

