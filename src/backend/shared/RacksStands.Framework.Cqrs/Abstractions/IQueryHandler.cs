namespace RacksStands.Framework.Cqrs.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;
