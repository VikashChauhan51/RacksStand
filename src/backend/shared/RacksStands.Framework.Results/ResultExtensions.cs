using Microsoft.AspNetCore.Http;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace RacksStands.Framework.Results;

public static class ResultExtensions
{
    public static IResult ToMinimalApiResult<TSuccess, TError>(this Result<TSuccess, TError> result)
    {
        return result.IsSuccess
            ? HttpResults.Ok(result.Success)
            : HttpResults.BadRequest(result.Error);
    }

    // Add more mappings as needed, e.g., for different status codes
}
