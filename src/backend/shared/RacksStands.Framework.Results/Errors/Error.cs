namespace RacksStands.Framework.Results.Errors;

public record Error(ErrorType Type, string Description)
{
    public static readonly Error Success = new(ErrorType.Success, "Operation completed successfully.");
    public static Error Failure(string description = "A failure occurred.") =>
        new(ErrorType.Failure, description);

    public static Error Validation(string description = "Validation error.") =>
        new(ErrorType.Validation, description);

    public static Error NotFound(string description = "Resource not found.") =>
        new(ErrorType.NotFound, description);

    public static Error Conflict(string description = "Conflict occurred.") =>
        new(ErrorType.Conflict, description);

    public static Error Unauthorized(string description = "Unauthorized access.") =>
        new(ErrorType.Unauthorized, description);

    public static Error Unexpected(string description = "An unexpected error occurred.") =>
        new(ErrorType.Unexpected, description);
}
