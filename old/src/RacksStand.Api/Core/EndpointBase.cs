using Carter;

namespace RacksStand.Api.Core;

public abstract class EndpointBase : CarterModule
{
    protected EndpointBase()
    {

    }
    public abstract string RouteName { get; }

    protected IResult Problem(Exception error)
    {
        return error switch
        {
            ValidationException validationException =>
            Results.ValidationProblem(validationException.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                g =>
                g.Select(e => e.ErrorMessage)
                .ToArray()
                ), validationException.Message),

            UnauthorizedAccessException => Results.Challenge(),
            _ => Results.BadRequest(error)
        };
    }
}
