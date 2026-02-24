using OneOf;

namespace RacksStands.Framework.Results;

public class Result<TSuccess, TError> : OneOfBase<TSuccess, TError>
{
    public Result(OneOf<TSuccess, TError> input) : base(input) { }

    public static implicit operator Result<TSuccess, TError>(TSuccess success) => new(success);
    public static implicit operator Result<TSuccess, TError>(TError error) => new(error);

    public bool IsSuccess => IsT0;
    public bool IsError => IsT1;

    public TSuccess Success => AsT0;
    public TError Error => AsT1;
}

public static class Result
{
    public static Result<TSuccess, TError> Success<TSuccess, TError>(TSuccess success) => success;
    public static Result<TSuccess, TError> Error<TSuccess, TError>(TError error) => error;
}
