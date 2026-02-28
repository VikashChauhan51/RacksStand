using System;

namespace RacksStands.Framework.Base.Exceptions;

public class RacksStandsBaseException : Exception
{
    public int StatusCode { get; }

    public RacksStandsBaseException(string message, int statusCode = 500) : base(message)
    {
        StatusCode = statusCode;
    }

    public RacksStandsBaseException(string message, Exception innerException, int statusCode = 500)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}

public class RacksStandsNotFoundException : RacksStandsBaseException
{
    public RacksStandsNotFoundException(string message) : base(message, 404) { }
}

public class RacksStandsBadRequestException : RacksStandsBaseException
{
    public RacksStandsBadRequestException(string message) : base(message, 400) { }
}

public class RacksStandsUnauthorizedException : RacksStandsBaseException
{
    public RacksStandsUnauthorizedException(string message) : base(message, 401) { }
}

public class RacksStandsForbiddenException : RacksStandsBaseException
{
    public RacksStandsForbiddenException(string message) : base(message, 403) { }
}

public class RacksStandsConflictException : RacksStandsBaseException
{
    public RacksStandsConflictException(string message) : base(message, 409) { }
}

public class RacksStandsTooManyRequestsException : RacksStandsBaseException
{
    public RacksStandsTooManyRequestsException(string message) : base(message, 429) { }
}

public class RacksStandsUnavailableException : RacksStandsBaseException
{
    public RacksStandsUnavailableException(string message) : base(message, 503) { }
}

public class RacksStandsValidationException : RacksStandsBaseException
{
    public RacksStandsValidationException(string message) : base(message, 400) { }
}

public class RacksStandsUnexpectedException : RacksStandsBaseException
{
    public RacksStandsUnexpectedException(string message) : base(message, 500) { }
}
