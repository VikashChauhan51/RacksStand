using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RacksStands.Framework.Results;

public static class ResultApiExtensions
{
    public static IResult ToApiResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            Errors.ErrorType.Success => Results.Ok(result.Value),
            Errors.ErrorType.Created => Results.Created(string.Empty, result.Value),
            Errors.ErrorType.NoContent => Results.NoContent(),
            Errors.ErrorType.NotFound => Results.NotFound(result.Exception?.Message),
            Errors.ErrorType.Validation => Results.BadRequest(result.Exception?.Message),
            Errors.ErrorType.Conflict => Results.Conflict(result.Exception?.Message),
            Errors.ErrorType.Unauthorized => Results.Unauthorized(),
            Errors.ErrorType.Forbidden => Results.StatusCode(StatusCodes.Status403Forbidden),
            _ => Results.Problem(result.Exception?.Message)
        };
    }

    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            Errors.ErrorType.Success => new OkObjectResult(result.Value),
            Errors.ErrorType.Created => new CreatedResult(string.Empty, result.Value),
            Errors.ErrorType.NoContent => new NoContentResult(),
            Errors.ErrorType.NotFound => new NotFoundObjectResult(result.Exception?.Message),
            Errors.ErrorType.Validation => new BadRequestObjectResult(result.Exception?.Message),
            Errors.ErrorType.Conflict => new ConflictObjectResult(result.Exception?.Message),
            Errors.ErrorType.Unauthorized => new UnauthorizedResult(),
            Errors.ErrorType.Forbidden => new ForbidResult(),
            _ => new ObjectResult(result.Exception?.Message) { StatusCode = StatusCodes.Status500InternalServerError }
        };
    }
}
