namespace RacksStands.Framework.Base.Exceptions;

public class BaseException : Exception
{
    public int StatusCode { get; }

    public BaseException(string message, int statusCode = 500) : base(message)
    {
        StatusCode = statusCode;
    }

    public BaseException(string message, Exception innerException, int statusCode = 500)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message, 404) { }
}

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base(message, 400) { }
}

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message) : base(message, 401) { }
}

public class ForbiddenException : BaseException
{
    public ForbiddenException(string message) : base(message, 403) { }
}
