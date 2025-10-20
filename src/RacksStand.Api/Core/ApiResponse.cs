namespace RacksStand.Api.Core;

public record ApiResponse(
    [property: JsonPropertyName("status_code")] int StatusCode,
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("metadata")] Metadata Metadata)
{
    public static ApiResponse Success(string message = "Request successful", Metadata? metadata = null)
        => new(StatusCodes.Status200OK, message, metadata ?? new());

    public static ApiResponse Fail(
        int statusCode = StatusCodes.Status400BadRequest,
        string? message = null,
        Metadata? metadata = null)
        => new(statusCode, message ?? "Request failed", metadata ?? new());
}

public record ApiResponse<T>(
    [property: JsonPropertyName("status_code")] int StatusCode,
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("data")] T? Data,
    [property: JsonPropertyName("metadata")] Metadata Metadata)
{
    public static ApiResponse<T> Success(T data, string message = "Request successful", Metadata? metadata = null)
        => new(StatusCodes.Status200OK, message, data, metadata ?? new());

    public static ApiResponse<T> Fail(
        int statusCode = StatusCodes.Status400BadRequest,
        string? message = null,
        Metadata? metadata = null)
        => new(statusCode, message ?? "Request failed", default, metadata ?? new());
}

/// <summary>
/// Represents optional metadata for API responses.
/// </summary>
public record Metadata(
    [property: JsonPropertyName("trace_id")] string? TraceId = null,
    [property: JsonPropertyName("version")] string? Version = null,
    [property: JsonPropertyName("pagination")] object? Pagination = null,
    [property: JsonPropertyName("attributes")] Dictionary<string, object>? Attributes = null);
