namespace RacksStand.Api.Core.Exceptions.Core;

using System.Net;


/// <summary>
/// The base exception for all application-level errors.
/// Provides HTTP metadata for standardized API responses.
/// </summary>
public abstract class ExceptionBase : Exception
{
    /// <summary>
    /// Gets the HTTP status code associated with this exception.
    /// </summary>
    public virtual int StatusCode { get; }

    /// <summary>
    /// Gets a short, human-readable title for this error.
    /// </summary>
    public virtual string Title { get; }

    /// <summary>
    /// Gets a URI reference that identifies the problem type.
    /// </summary>
    public virtual string Type { get; }

    /// <summary>
    /// Additional context that can be logged or returned for debugging.
    /// </summary>
    public virtual IDictionary<string, object>? Metadata { get; init; }

    protected ExceptionBase(string message, int statusCode, string title, string type)
        : base(message)
    {
        StatusCode = statusCode;
        Title = title;
        Type = type;
    }
}


