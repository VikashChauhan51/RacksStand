using System;

namespace RacksStands.Framework.Base.Exceptions;

public class RacksStandsBaseException : Exception
{
    public int StatusCode { get; }

    public IReadOnlyDictionary<string, object> Errors { get; }

    public RacksStandsBaseException(string message, int statusCode = 500) : base(message)
    {
        StatusCode = statusCode;
        Errors = new Dictionary<string, object>()
        {
            [""] = message
        };
    }

    public RacksStandsBaseException(string message, Exception innerException, int statusCode = 500)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Errors = new Dictionary<string, object>()
        {
            [""] = message
        };
    }

    public RacksStandsBaseException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 500)
        : base(message)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}

public class RacksStandsNotFoundException : RacksStandsBaseException
{
    public RacksStandsNotFoundException(string message) : base(message, 404) { }
    public RacksStandsNotFoundException(string message, Exception innerException) : base(message, innerException, 404) { }
    public RacksStandsNotFoundException(string message, IReadOnlyDictionary<string, object> errors) : base(message, errors, 404) { }
}

public class RacksStandsBadRequestException : RacksStandsBaseException
{
    public RacksStandsBadRequestException(string message = "Bad request.") : base(message, 400) { }
    public RacksStandsBadRequestException(string message, Exception innerException) : base(message, innerException, 400) { }
    public RacksStandsBadRequestException(string message, IReadOnlyDictionary<string, object> errors) : base(message, errors, 400) { }
}

public class RacksStandsUnauthorizedException : RacksStandsBaseException
{
    public RacksStandsUnauthorizedException(string message = "Unauthorized.") : base(message, 401) { }
    public RacksStandsUnauthorizedException(string message, Exception innerException) : base(message, innerException, 401) { }
    public RacksStandsUnauthorizedException(string message, IReadOnlyDictionary<string, object> errors) : base(message, errors, 401) { }
}

public class RacksStandsForbiddenException : RacksStandsBaseException
{
    public RacksStandsForbiddenException(string message = "Forbidden.") : base(message, 403) { }
    public RacksStandsForbiddenException(string message, Exception innerException) : base(message, innerException, 403) { }
    public RacksStandsForbiddenException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 403) : base(message, errors, statusCode) { }
}

public class RacksStandsConflictException : RacksStandsBaseException
{
    public RacksStandsConflictException(string message = "Conflict.") : base(message, 409) { }
    public RacksStandsConflictException(string message, Exception innerException) : base(message, innerException, 409) { }
    public RacksStandsConflictException(string message, IReadOnlyDictionary<string, object> errors) : base(message, errors, 409) { }
}

public class RacksStandsUnprocessableEntityException : RacksStandsBaseException
{
    public RacksStandsUnprocessableEntityException(string message = "Unprocessable entity.") : base(message, 422) { }
    public RacksStandsUnprocessableEntityException(string message, Exception innerException) : base(message, innerException, 422) { }
    public RacksStandsUnprocessableEntityException(string message, IReadOnlyDictionary<string, object> errors) : base(message, errors, 422) { }
}

public class RacksStandsTooManyRequestsException : RacksStandsBaseException
{
    public RacksStandsTooManyRequestsException(string message = "Too many request.") : base(message, 429) { }
    public RacksStandsTooManyRequestsException(string message, Exception innerException) : base(message, innerException, 429) { }
    public RacksStandsTooManyRequestsException(string message, IReadOnlyDictionary<string, object> errors) : base(message, errors, 429) { }
}

public class RacksStandsUnavailableException : RacksStandsBaseException
{
    public RacksStandsUnavailableException(string message) : base(message, 503) { }
    public RacksStandsUnavailableException(string message, Exception innerException) : base(message, innerException, 503) { }
    public RacksStandsUnavailableException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 500) : base(message, errors, statusCode) { }
}

public class RacksStandsValidationException : RacksStandsBaseException
{
    public RacksStandsValidationException(string message = "Unavailable.") : base(message, 400) { }
    public RacksStandsValidationException(string message, Exception innerException) : base(message, innerException, 400) { }
    public RacksStandsValidationException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 400) : base(message, errors, statusCode) { }
}

public class RacksStandsUnexpectedException : RacksStandsBaseException
{
    public RacksStandsUnexpectedException(string message = "An unexpected error occurred.") : base(message, 500) { }
    public RacksStandsUnexpectedException(string message, Exception innerException) : base(message, innerException, 500) { }
    public RacksStandsUnexpectedException(string message, IReadOnlyDictionary<string, object> errors, int statusCode = 500) : base(message, errors, statusCode) { }
}
