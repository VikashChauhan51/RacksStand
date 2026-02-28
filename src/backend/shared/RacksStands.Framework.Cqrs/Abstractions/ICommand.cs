namespace RacksStands.Framework.Cqrs.Abstractions;

public interface ICommand<TResponse> : IRequest<TResponse>;

public interface ICommand : ICommand<Unit>;
