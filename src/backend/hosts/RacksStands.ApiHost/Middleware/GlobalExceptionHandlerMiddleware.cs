using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RacksStands.Framework.Base.Exceptions;
using RacksStands.Framework.Base.Extensions;

namespace RacksStands.ApiHost.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            BaseException baseEx => (baseEx.StatusCode, baseEx.Message),
            KeyNotFoundException => (404, "Resource not found"),
            ArgumentException => (400, "Invalid request"),
            UnauthorizedAccessException => (401, "Unauthorized"),
            _ => (500, "An unexpected error occurred")
        };

        _logger.LogError(exception, "An error occurred: {Message}", exception.Message.SanitizeForLogging());

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            error = message,
            timestamp = DateTime.UtcNow
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}
