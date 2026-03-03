using RacksStands.Framework.Base.Exceptions;
using RacksStands.Framework.Base.Serializers;
using RacksStands.Framework.Results;
using System.Diagnostics;

namespace RacksStands.ApiHost.Middlewares;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private  Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (context.Response.HasStarted)
        {
            return Task.CompletedTask;
        }
        var (statusCode, apiResponse) = BuildApiResponse(context, exception);
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsJsonAsync(apiResponse, JsonSerializerHelper.Options);
    }

    private static (int statusCode, ApiResponse) BuildApiResponse(
        HttpContext context,
        Exception exception)
    {
        var trackingId = context.TraceIdentifier ?? Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();
        var correlationId = context.Request.Headers.TryGetValue("X-Correlation-ID", out var correlationIdHeader) ? correlationIdHeader.ToString() : null;

        var meta = new Dictionary<string, object>(StringComparer.Ordinal);
        if (!string.IsNullOrEmpty(correlationId))
        {
            meta["CorrelationId"] = correlationId;
        }
        meta["TrackingId"] = trackingId;

        return exception switch
        {
            RacksStandsBaseException ex =>
            (
                ex.StatusCode,
                new ApiResponse
                {
                    Success = false,
                    Error = ex.Message,
                    ErrorMeta = MergeFieldErrors(meta, ex.Errors)
                }
            ),
            _ =>
            (
                StatusCodes.Status500InternalServerError,
                new ApiResponse
                {
                    Success = false,
                    Error = "An unexpected error occurred.",
                    ErrorMeta = meta
                }
            )
        };
    }

    private static IReadOnlyDictionary<string, object> MergeFieldErrors(
        Dictionary<string, object> meta,
        IReadOnlyDictionary<string, object> fieldErrors)
    {
        foreach (var error in fieldErrors)
        {
            meta[error.Key] = error.Value;
        }
        return meta;
    }
}
