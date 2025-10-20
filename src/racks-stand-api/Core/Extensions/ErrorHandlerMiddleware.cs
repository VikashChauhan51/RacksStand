namespace RacksStand.Api.Core.Extensions;

using Microsoft.Extensions.Logging;
using RacksStand.Api.Core.Exceptions.Core;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
        _logger.LogError(exception, "An unhandled exception has occurred");

        HttpValidationProblemDetails problemDetails;
        if (exception is ExceptionBase exBase)
        {
            problemDetails = new HttpValidationProblemDetails
            {
                Status = exBase.StatusCode,
                Title = exBase.Title,
                Detail = exBase.Message,
                Type = exBase.Type,
                Instance = context.Request.Path
            };

            // handle validation errors
            if (exBase is RacksStand.Api.Core.Exceptions.ValidationException validation && validation.Errors.Any())
            {
                problemDetails.Errors = validation.Errors;
            }

            // include metadata if present
            if (exBase.Metadata is not null && exBase.Metadata.Count > 0)
            {
                problemDetails.Extensions["metadata"] = exBase.Metadata;
            }
            context.Response.StatusCode = exBase.StatusCode;
        }
        else
        {
            // Determine HTTP status code
            var statusCode = exception switch
            {
                System.ComponentModel.DataAnnotations.ValidationException => StatusCodes.Status400BadRequest,
                FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                InvalidOperationException => StatusCodes.Status409Conflict,
                ArgumentException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;
            problemDetails = new HttpValidationProblemDetails
            {
                Status = statusCode,
                Title = GetTitleForStatus(statusCode),
                Detail = exception.Message,
                Instance = context.Request.Path,
                Type = GetTypeForStatus(statusCode)
            };

            // FluentValidation errors
            if (exception is FluentValidation.ValidationException fluentValidation)
            {
                var errors = fluentValidation.Errors?
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                if (errors != null)
                    problemDetails.Errors = errors;
            }

            // DataAnnotations validation
            else if (exception is System.ComponentModel.DataAnnotations.ValidationException dataAnnotationValidation)
            {
                problemDetails.Errors = new Dictionary<string, string[]>
                {
                    ["ValidationError"] = new[] { dataAnnotationValidation.Message }
                };
            }
        }

        await context.Response.WriteAsJsonAsync(
            problemDetails,
            new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            },
            MediaTypeNames.Application.ProblemJson
        );
    }

    private static string GetTitleForStatus(int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => "Validation Error",
        StatusCodes.Status401Unauthorized => "Unauthorized",
        StatusCodes.Status403Forbidden => "Access Denied",
        StatusCodes.Status409Conflict => "Request Conflict",
        StatusCodes.Status422UnprocessableEntity => "Unprocessable Entity",
        StatusCodes.Status429TooManyRequests => "Too Many Requests",
        _ => "Internal Server Error"
    };

    private static string GetTypeForStatus(int statusCode) => statusCode switch
    {
        StatusCodes.Status400BadRequest => "https://httpstatuses.com/400",
        StatusCodes.Status401Unauthorized => "https://httpstatuses.com/401",
        StatusCodes.Status403Forbidden => "https://httpstatuses.com/403",
        StatusCodes.Status409Conflict => "https://httpstatuses.com/409",
        StatusCodes.Status422UnprocessableEntity => "https://httpstatuses.com/422",
        StatusCodes.Status429TooManyRequests => "https://httpstatuses.com/429",
        _ => "https://httpstatuses.com/500"
    };
}

